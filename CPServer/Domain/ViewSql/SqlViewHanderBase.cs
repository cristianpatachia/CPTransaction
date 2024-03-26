using CPServer.Domain.Context;

namespace CPServer.Domain.ViewSql;

public class SqlViewHandlerBase
{
    protected readonly AppDbContext DbContext;

    public SqlViewHandlerBase(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async virtual void AddEntity<T>(T entity) 
        where T : class
    {
        await DbContext.AddAsync<T>(entity);

        await DbContext.SaveChangesAsync();
    }
}