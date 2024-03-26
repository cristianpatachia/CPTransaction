using CPClient.Model;
using CPClient.Services.GrpcClientServiceBase;
using CPClient.Services.Interfaces;
using Grpc.Core;

namespace CPClient.Services.Impl
{
    public class TransactionGrpcClientService : GrpcClientServiceBase<Transaction.TransactionClient>, ITransactionGrpcClientService
    {
        public TransactionGrpcClientService(IConfiguration configuration) 
            : base(configuration)
        {
        }

        public async Task<CreateTransactionResponse> CreateTransactionRequest(TransactionModel model)
        {
            var clientRequested = new CreateTransactionRequest
            {
                SenderId = model.SenderIdString,
                RecipientId = model.RecipientIdString,
                Amount = model.Amount.ToString(),
                Timestamp = model.Timestamp.ToString(),
                Details = model.Details
            };

            var transactionResponse = await _client.CreateTransactionAsync(clientRequested);

            return transactionResponse;
        }

        public async Task<List<ReadTransactionResponse>> ReadTransactionHistoryRequest(AccountModel model)
        {
            var request = new ReadTransactionListRequest
            {
                AccountId = model.Id
            };

            var result = new List<ReadTransactionResponse>();

            using (var call = _client.ReadTransactionList(request))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var item = call.ResponseStream.Current;
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
