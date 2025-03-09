using Clothing.CMS.Entities.Common;

namespace Clothing.CMS.Application.Customers.Dto
{
    public class CustomerDto : BaseEntity
    {
		public string FullName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Address { get; set; }
	}
}
