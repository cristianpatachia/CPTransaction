using CPServer.Domain.ViewSql.Transaction;

namespace CPServer.Domain.Services.Interfaces
{
    public interface ITransactionDataService
    {
        Task<IEnumerable<TransactionSqlView>> GetTransactionsAsync(string accountIdString);
    }
}