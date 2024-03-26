using CPServer.Domain.Context;
using CPServer.Domain.Services.Interfaces;
using CPServer.Domain.ViewSql.Account;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CPServer.Domain.Services.Impl
{
    public class AccountDataService : IAccountDataService
    {
        private readonly AppDbContext dbContext;

        public AccountDataService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<AccountSqlView> GetCustomerAccounts()
        {
            return dbContext.Accounts.AsNoTracking();
        }

        public List<SelectListItem> GetCustomerAccountsDropdown()
        {
            return this.GetCustomerAccounts()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .ToList();
        }

        public async Task<IEnumerable<AccountSqlView>> GetCustomerAccountsAsync()
        {
            return await dbContext.Accounts.AsNoTracking().ToListAsync();
        }

        public async Task<List<SelectListItem>> GetCustomerAccountsDropdownAsync()
        {
            var result = new List<SelectListItem>();

            foreach (var customerAccount in await this.GetCustomerAccountsAsync())
            {
                result.Add(new SelectListItem
                {
                    Text = customerAccount.Name,
                    Value = customerAccount.Id.ToString()
                });
            }

            return result;
        }
    }
}
