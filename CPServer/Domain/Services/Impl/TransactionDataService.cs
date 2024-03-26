using CPServer.Domain.Context;
using CPServer.Domain.Services.Interfaces;
using CPServer.Domain.ViewSql.Transaction;
using Microsoft.EntityFrameworkCore;

namespace CPServer.Domain.Services.Impl;

public class TransactionDataService : ITransactionDataService
{
    private readonly AppDbContext appDbContext;

    public TransactionDataService(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<IEnumerable<TransactionSqlView>> GetTransactionsAsync(string accountIdString)
    {
        if (accountIdString is null || !Guid.TryParse(accountIdString, out var accountId)) 
        {
            return Enumerable.Empty<TransactionSqlView>();
        }

        return await appDbContext.Transactions
            .AsNoTracking()
            .Where(x => x.SenderId == accountId)
            .ToListAsync();
    }
}
