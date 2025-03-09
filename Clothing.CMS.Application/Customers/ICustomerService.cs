using Clothing.CMS.Application.Customers.Dto;

namespace Clothing.CMS.Application.Customers
{
    public interface ICustomerService
    {
		Task<ICollection<CustomerDto>> GetAll();
	}
}
