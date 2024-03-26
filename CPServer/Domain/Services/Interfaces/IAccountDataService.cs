using CPServer.Domain.ViewSql.Account;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CPServer.Domain.Services.Interfaces
{
    public interface IAccountDataService
    {
        IEnumerable<AccountSqlView> GetCustomerAccounts();

        List<SelectListItem> GetCustomerAccountsDropdown();

        Task<IEnumerable<AccountSqlView>> GetCustomerAccountsAsync();

        Task<List<SelectListItem>> GetCustomerAccountsDropdownAsync();
    }
}