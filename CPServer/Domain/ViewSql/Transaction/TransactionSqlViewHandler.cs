using CPServer.Domain.Context;
using CPServer.Domain.ViewSql;

namespace Domain.ViewSql.Account;

public class TransactionSqlViewHandler : SqlViewHandlerBase
{
    public TransactionSqlViewHandler(AppDbContext dbContext) 
        : base(dbContext)
    {
        
    }
}
