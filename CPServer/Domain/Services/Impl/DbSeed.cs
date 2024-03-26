using CPServer.Domain.Context;
using CPServer.Domain.ValueObjects.Enums;
using CPServer.Domain.ViewSql.Account;
using CPServer.Domain.ViewSql.Customer;
using CPServer.Domain.ViewSql.Transaction;
using Microsoft.EntityFrameworkCore;

namespace CPServer.Domain.Services.Impl
{
    public class DbSeed : IDbSeed
    {
        private readonly AppDbContext dbContext;

        public DbSeed(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Initialize()
        {
            if (!dbContext.Customers.AsNoTracking().Any()
                && !dbContext.Accounts.AsNoTracking().Any())
            {
                SeedCustomers(out var customers);
                SeedAccounts(customers);

                await dbContext.SaveChangesAsync();
            }


            await SeedTransactions();
        }

        private async Task SeedTransactions()
        {
            var totalExpectedTransactions = 0;
            var currentTransactions = dbContext.Transactions.AsNoTracking().Count();
            var targetTransactionsToBeAdded = totalExpectedTransactions - currentTransactions;

            if (targetTransactionsToBeAdded < 0)
            {
                return;
            }

            var senderGuid = new Guid("0FAE8D7F-8868-4CDC-ADB0-ACC556E57C0B");
            var recipientGuid = new Guid("4D1DA59E-512F-450F-AD00-E2876D723068");
            var utcNow = DateTime.UtcNow;

            var transactions = new List<TransactionSqlView>();
            for (var i = 1; i <= targetTransactionsToBeAdded; i++)
            {
                var transactionItem = new TransactionSqlView
                {
                    Id = Guid.NewGuid(),
                    Amount = i,
                    Details = i.ToString(),
                    ReceivedUtcDateTime = utcNow.AddSeconds(i + 1),
                    SenderId = senderGuid,
                    RecipientId = recipientGuid,
                    Status = TransactionStatus.Pending,
                    Timestamp = utcNow.AddSeconds(i),
                };

                transactions.Add(transactionItem);
            }

            await dbContext.AddRangeAsync(transactions);

            await dbContext.SaveChangesAsync();
        }

        private void SeedCustomers(out List<CustomerSqlView> customers)
        {
            customers = new List<CustomerSqlView>();
            var nameList = new List<string>
            {
                "Harold",
                "Irving",
                "Walter",
                "Terry",
                "Christopher"
            };

            foreach (var name in nameList)
            {
                customers.Add(new CustomerSqlView
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                });
            }

            dbContext.Customers.AddRangeAsync(customers);
        }

        private void SeedAccounts(List<CustomerSqlView> customers)
        {
            var accounts = new List<AccountSqlView>();

            foreach (var customer in customers)
            {
                var random = new Random();
                accounts.Add(new AccountSqlView
                {
                    Id = Guid.NewGuid(),
                    Name = "Current ({0})".F(customer.Name),
                    CustomerId = customer.Id,
                    Balance = random.Next(1000, 10000),
                });
            }

            dbContext.AddRangeAsync(accounts);
        }
    }
}
