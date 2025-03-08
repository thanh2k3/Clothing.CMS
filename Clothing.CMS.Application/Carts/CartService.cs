using Clothing.CMS.Application.Carts.Dto;
using Clothing.CMS.Application.Common;
using Clothing.CMS.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Clothing.CMS.Application.Carts
{
	public class CartService : BaseService, ICartService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CartService(IHttpContextAccessor httpContextAccessor,
			ITempDataDictionaryFactory tempDataDictionaryFactory) : base(httpContextAccessor, tempDataDictionaryFactory)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		private ISession _session => _httpContextAccessor.HttpContext.Session;

		public async Task<bool> AddToCartAsync(CartItemDto model)
		{
			try
			{
				if (model == null || model.ProductId <= 0)
				{
					NotifyMsg("Thông tin không hợp lệ");
					return false;
				}

				// Lấy giỏ hàng từ session
				var cartItems = _session.Get<List<CartItemDto>>(SessionConstants.CART_SESSION_KEY) ?? new List<CartItemDto>();

				var existingItem = cartItems.FirstOrDefault(x => x.ProductId == model.ProductId && x.Name == model.Name
															&& x.Size == model.Size && x.Color == model.Color);
				if (existingItem != null)
				{
					existingItem.Quantity += model.Quantity;
				}
				else
				{
					cartItems.Add(new CartItemDto
					{
						ProductId = model.ProductId,
						Name = model.Name,
						ImageURL = model.ImageURL,
						Price = model.Price,
						Quantity = model.Quantity,
						Size = model.Size,
						Color = model.Color,
					});
				}

				// Lưu giỏ hàng vào session
				_session.Set(SessionConstants.CART_SESSION_KEY, cartItems);

				// Lưu số mặt hàng vào session
				_session.Set(SessionConstants.CART_PRODUCT_COUNT, cartItems.Count);

				NotifyMsg("Đã được thêm vào giỏ hàng");
				return true;
			}
			catch (Exception ex)
			{
				NotifyMsg("Thêm sản phẩm vào giỏ hàng thất bại");
				return false;
			}
		}

		public async Task<List<CartItemDto>> GetListItemAsync()
		{
			try
			{
				var cartItems = _session.Get<List<CartItemDto>>(SessionConstants.CART_SESSION_KEY) ?? new List<CartItemDto>();

				return cartItems;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<int> GetCartProductCountAsync()
		{
			try
			{
				var result = _session.Get<int>(SessionConstants.CART_PRODUCT_COUNT);

				return result;
			}
			catch (Exception ex)
			{
				throw new Exception();
			}
		}

		public async Task<bool> RemoveFromCartAsync(int productId, string name, string size, string color)
		{
			try
			{
				var cartItems = _session.Get<List<CartItemDto>>(SessionConstants.CART_SESSION_KEY) ?? new List<CartItemDto>();
				var itemRemove = cartItems.FirstOrDefault(x => x.ProductId == productId && x.Name == name
														  && x.Size == size && x.Color == color);
				if (itemRemove == null)
				{
					NotifyMsg("Không tìm thấy sản phẩm trong giỏ hàng");
					return false;
				}

				cartItems.Remove(itemRemove);
				_session.Set(SessionConstants.CART_SESSION_KEY, cartItems);

				// Cập nhật lại số mặt hàng vào session
				_session.Set(SessionConstants.CART_PRODUCT_COUNT, cartItems.Count);

				NotifyMsg("Đã xóa sản phẩm khỏi giỏ hàng");
				return true;
			}
			catch (Exception ex)
			{
				NotifyMsg("Xóa sản phẩm thất bại");
				return false;
			}
		}

		public async Task<bool> UpdateCartAsync(int productId, string name, string size, string color, int quantity)
		{
			try
			{
				var cartItems = _session.Get<List<CartItemDto>>(SessionConstants.CART_SESSION_KEY) ?? new List<CartItemDto>();
				var item = cartItems.FirstOrDefault(x => x.ProductId == productId && x.Name == name
													&& x.Size == size && x.Color == color);
				if (item == null)
				{
					NotifyMsg("Không tìm thấy sản phẩm trong giỏ hàng");
					return false;
				}

				item.Quantity = quantity;

				_session.Set(SessionConstants.CART_SESSION_KEY, cartItems);

				NotifyMsg("Cập nhật giỏ hàng thành công");
				return true;
			}
			catch (Exception ex)
			{
				NotifyMsg("Cập nhật giỏ hàng thất bại");
				return false;
			}
		}

		public async Task<bool> ClearSessionAsync()
		{
			try
			{
				_session.Remove(SessionConstants.CART_SESSION_KEY);
				_session.Remove(SessionConstants.CART_PRODUCT_COUNT);

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
