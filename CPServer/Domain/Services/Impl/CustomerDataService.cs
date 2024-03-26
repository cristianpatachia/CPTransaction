using CPServer.Domain.Context;
using CPServer.Domain.Services.Interfaces;
using CPServer.Domain.ViewSql.Customer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CPServer.Domain.Services.Impl
{
    public class CustomerDataService : ICustomerDataService
    {
        private readonly AppDbContext dbContext;

        public CustomerDataService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Guid> GetCustomerIds()
        {
            return dbContext.Customers.AsNoTracking()
                .Select(c => c.Id);
        }

        public async Task<IEnumerable<Guid>> GetCustomerIdsAsync()
        {
            return await dbContext.Customers.AsNoTracking()
                .Select(x => x.Id)
                .ToListAsync();
        }

        public IEnumerable<CustomerSqlView> GetCustomers()
        {
            return dbContext.Customers.AsNoTracking();
        }

        public List<SelectListItem> GetCustomersDropdown()
        {
            return this.GetCustomers()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .ToList();
        }
    }
}
