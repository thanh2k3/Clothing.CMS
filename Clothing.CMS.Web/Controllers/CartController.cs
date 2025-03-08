using AutoMapper;
using Clothing.CMS.Application.Carts;
using Clothing.CMS.Web.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Controllers
{
	public class CartController : Controller
	{
		private readonly ICartService _cartService;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;

		public CartController(ICartService cartService,
			IMapper mapper,
			ILoggerFactory loggerFactory)
		{
			_cartService = cartService;
			_mapper = mapper;
			_logger = loggerFactory.CreateLogger<CartController>();
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<JsonResult> GetCart()
		{
			try
			{
				var cartItems = await _cartService.GetListItemAsync();
				var cartItemVM = _mapper.Map<List<CartItemViewModel>>(cartItems);

				_logger.LogInformation("Xem giỏ hàng");
				return Json(new { success = true, cartItemVM });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return Json(new { success = false });
			}
		}

		[HttpPost]
		public async Task<JsonResult> UpdateCart(int productId, string name, string size, string color, int quantity)
		{
			try
			{
				var isSucceeded = await _cartService.UpdateCartAsync(productId, name, size, color, quantity);
				if (!isSucceeded)
				{
					_logger.LogWarning((string?)TempData["Message"]);
					return Json(new { success = false, message = TempData["Message"] });
				}

				_logger.LogInformation((string?)TempData["Message"]);
				return Json(new { success = true, message = TempData["Message"] });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return Json(new { success = false, message = "Có lỗi xảy ra" });
			}
		}

		[HttpPost]
		public async Task<JsonResult> RemoveFromCart(int productId, string name, string size, string color)
		{
			try
			{
				var isSucceeded = await _cartService.RemoveFromCartAsync(productId, name, size, color);
				if (!isSucceeded)
				{
					_logger.LogWarning((string?)TempData["Message"]);
					return Json(new { success = false, message = TempData["Message"] });
				}

				_logger.LogInformation((string?)TempData["Message"]);
				return Json(new { success = true, message = TempData["Message"] });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return Json(new { success = false, message = "Có lỗi xảy ra" });
			}
		}

		[HttpPost]
		public async Task<JsonResult> ClearSession()
		{
			try
			{
				var isSucceeded = await _cartService.ClearSessionAsync();
				if (!isSucceeded)
				{
					_logger.LogWarning("Session của giỏ hàng chưa được xóa");
					return Json(new { success = false });
				}

				_logger.LogInformation("Session của giỏ hàng đã được xóa");
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return Json(new { success = false, message = "Có lỗi xảy ra" });
			}
		}
	}
}
