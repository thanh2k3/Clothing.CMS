using Clothing.CMS.Application.Orders.Dto;

namespace Clothing.CMS.Application.Orders
{
	public interface IOrderService
	{
		Task<ICollection<OrderDto>> GetAll();
	}
}
