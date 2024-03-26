using Microsoft.AspNetCore.Mvc.Rendering;

namespace CPClient.Model
{
    public class CustomerModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string IdString { get; set; }

        public string AccountIdString { get; set; }

        public List<SelectListItem> AccountDropdownList { get; set; }
    }
}
