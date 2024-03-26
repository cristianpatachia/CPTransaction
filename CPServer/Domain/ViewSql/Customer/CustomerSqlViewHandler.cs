using CPServer.Domain.Context;
using CPServer.Domain.ViewSql;

namespace Domain.ViewSql.Account;

public class CustomerSqlViewHandler : SqlViewHandlerBase
{
    public CustomerSqlViewHandler(AppDbContext dbContext) 
        : base(dbContext)
    {
        
    }
}
