using CPServer.Domain.ViewSql.Customer;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CPServer.Domain.Services.Interfaces
{
    public interface ICustomerDataService
    {
        IEnumerable<Guid> GetCustomerIds();

        Task<IEnumerable<Guid>> GetCustomerIdsAsync();

        IEnumerable<CustomerSqlView> GetCustomers();

        List<SelectListItem> GetCustomersDropdown();
    }
}