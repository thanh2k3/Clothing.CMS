using Clothing.CMS.Application.Orders.Dto;

namespace Clothing.CMS.Application.Orders
{
	public interface IOrderService
	{
		Task<ICollection<OrderDto>> GetAll();
		Task<EditOrderDto> GetById(int id);
		Task<bool> CreateAsync(CreateOrderDto model);
		Task<bool> UpdateAsync(EditOrderDto model);
		Task<bool> DeleteAsync(int id);

	}
}
