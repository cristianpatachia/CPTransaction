using CPServer.Domain;
using CPServer.Domain.Context;
using CPServer.Domain.Helpers.Extensions;
using CPServer.Domain.Helpers.Validators;
using CPServer.Domain.Services.Interfaces;
using CPServer.Domain.ValueObjects.Enums;
using CPServer.Domain.ViewSql.Transaction;
using FluentValidation.Results;
using Grpc.Core;

namespace CPServer.GrpcServices
{
    public class TransactionGrpcService : Transaction.TransactionBase
    {
        private readonly AppDbContext dbContext;
        private readonly ITransactionDataService transactionDataService;
        private readonly ILogger<TransactionGrpcService> _logger;

        public TransactionGrpcService(
            AppDbContext dbContext,
            ITransactionDataService transactionDataService,
            ILogger<TransactionGrpcService> logger)
        {
            this.dbContext = dbContext;
            this.transactionDataService = transactionDataService;
            _logger = logger;
        }

        public override Task<ReadTransactionResponse> ReadTransaction(ReadTransactionRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ReadTransactionResponse());
        }

        public override async Task ReadTransactionList(
            ReadTransactionListRequest request,
            IServerStreamWriter<ReadTransactionResponse> responseStream,
            ServerCallContext context)
        {
            foreach (var transaction in await transactionDataService.GetTransactionsAsync(request.AccountId))
            {
                await responseStream.WriteAsync(
                    new ReadTransactionResponse
                    {
                        RecipientId = transaction.RecipientId.ToString(),
                        Amount = transaction.Amount.ToString(),
                        Details = transaction.Details,
                        Timestamp = transaction.Timestamp.ToString(),
                        TransactionStatus = transaction.Status.ToString(),
                    });
            }
        }

        public override Task<CreateTransactionResponse> CreateTransaction(CreateTransactionRequest request, ServerCallContext context)
        {
            ValidateTransaction(request, out bool isValid, out ValidationResult validationResult);
            List<string> validationErrors = [];

            if (isValid)
            {
                LogInfo(request);
                AddTransaction(request);
            }
            else
            {
                validationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            return Task.FromResult(new CreateTransactionResponse
            {
                IsSuccess = isValid ? 1 : 0,
                Message = GetTransactionResponseMessage(isValid, validationErrors, request)
            });
        }

        #region Private Methods

        private string GetTransactionResponseMessage(
            bool isValid,
            List<string> validationErrors,
            CreateTransactionRequest request)
        {
            return isValid || validationErrors.Count == 0
                ? "Transaction Created Successfully. Amount {0} is being processed to {1}".F(request.Amount, request.RecipientId)
                : "Exception ocurred while trying to process the transaction. Details: ({0})".F(validationErrors.ToDelimiterSeparatedValues());
        }

        private Task AddTransaction(CreateTransactionRequest request)
        {
            var transaction = new TransactionSqlView
            {
                Id = Guid.NewGuid(),
                Amount = decimal.Parse(request.Amount),
                SenderId = Guid.Parse(request.SenderId),
                RecipientId = Guid.Parse(request.RecipientId),
                Timestamp = DateTime.Parse(request.Timestamp),
                Details = request.Details,
                Status = TransactionStatus.Pending
            };

            dbContext.Transactions.AddAsync(transaction);

            dbContext.SaveChangesAsync();

            return Task.CompletedTask;
        }

        private void ValidateTransaction(
            CreateTransactionRequest request,
            out bool isValid,
            out ValidationResult validationResult)
        {
            var validator = new TransactionValidator();
            validationResult = validator.Validate(request);
            isValid = validationResult.IsValid;
        }

        private void LogInfo(CreateTransactionRequest request)
        {
            const string message = "Processing CreateTransaction request: SenderId: '{0}', RecipientId: '{1}', Amount: '{2}', Details: '{3}', Timestamp: '{4}''";

            _logger.LogInformation(
                message: message,
                args: new[]
                {
                    request.SenderId,
                    request.RecipientId,
                    request.Amount,
                    request.Details,
                    request.Timestamp
                });
        }

        #endregion
    }
}
