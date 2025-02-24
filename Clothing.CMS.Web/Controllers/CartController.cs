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

		// Hiển thị giỏ hàng
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
	}
}
