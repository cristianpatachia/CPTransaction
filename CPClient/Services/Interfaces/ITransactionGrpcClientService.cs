using CPClient.Model;

namespace CPClient.Services.Interfaces;

public interface ITransactionGrpcClientService
{
    Task<CreateTransactionResponse> CreateTransactionRequest(TransactionModel model);

    Task<List<ReadTransactionResponse>> ReadTransactionHistoryRequest(AccountModel model);
}