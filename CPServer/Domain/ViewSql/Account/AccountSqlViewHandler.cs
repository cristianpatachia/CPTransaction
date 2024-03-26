using CPServer.Domain.Context;
using CPServer.Domain.ViewSql;

namespace Domain.ViewSql.Account;

public class AccountSqlViewHandler : SqlViewHandlerBase
{
    public AccountSqlViewHandler(AppDbContext dbContext) 
        : base(dbContext)
    {
        
    }
}
