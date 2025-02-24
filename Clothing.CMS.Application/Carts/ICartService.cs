using Clothing.CMS.Application.Carts.Dto;

namespace Clothing.CMS.Application.Carts
{
	public interface ICartService
	{
		Task<bool> AddToCartAsync(CartItemDto model);
		Task<List<CartItemDto>> GetListItemAsync();
		Task<int> GetCartProductCountAsync();
	}
}
