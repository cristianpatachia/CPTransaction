using CPServer.Domain.ViewSql.Account;
using CPServer.Domain.ViewSql.Customer;
using CPServer.Domain.ViewSql.Transaction;
using Microsoft.EntityFrameworkCore;

namespace CPServer.Domain.Context;

public partial class AppDbContext : DbContext
{
    public DbSet<CustomerSqlView> Customers => Set<CustomerSqlView>();

    public DbSet<AccountSqlView> Accounts => Set<AccountSqlView>();

    public DbSet<TransactionSqlView> Transactions => Set<TransactionSqlView>();
}