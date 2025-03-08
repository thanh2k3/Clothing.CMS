using Clothing.CMS.Application.Carts.Dto;

namespace Clothing.CMS.Application.Carts
{
	public interface ICartService
	{
		Task<bool> AddToCartAsync(CartItemDto model);
		Task<List<CartItemDto>> GetListItemAsync();
		Task<int> GetCartProductCountAsync();
		Task<bool> UpdateCartAsync(int productId, string name, string size, string color, int quantity);
		Task<bool> RemoveFromCartAsync(int productId, string name, string size, string color);
		Task<bool> ClearSessionAsync();
	}
}
