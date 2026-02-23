using AutoMapper;
using Clothing.CMS.Application.Carts;
using Clothing.CMS.Application.Carts.Dto;
using Clothing.CMS.Application.Products;
using Clothing.CMS.Web.ViewModels.Cart;
using Clothing.CMS.Web.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService _productService;
		private readonly ICartService _cartService;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;

		public ProductController(IProductService productService,
			ICartService cartService,
			IMapper mapper,
			ILoggerFactory loggerFactory)
		{
			_productService = productService;
			_cartService = cartService;
			_mapper = mapper;
			_logger = loggerFactory.CreateLogger<ProductController>();
		}

		//public async Task<IActionResult> Index()
		//{
		//	try
		//	{
		//		var productDto = await _productService.GetAll();
		//		var productVM = _mapper.Map<ICollection<ProductViewModel>>(productDto);

		//		_logger.LogInformation("Lấy ra tất cả sản phẩm");
		//		return View(productVM);
		//	}
		//	catch (Exception ex)
		//	{
		//		_logger.LogError(ex.Message);
		//		return View();
		//	}
		//}

		public async Task<IActionResult> ProductDetail(int id)
		{
			try
			{
				var productDto = await _productService.GetById(id);
				var productVM = _mapper.Map<EditProductViewModel>(productDto);

				return View(productVM);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return View();
			}
		}

		[HttpPost]
		public async Task<JsonResult> AddToCart(CartItemViewModel model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					_logger.LogWarning("Thông tin không hợp lệ");
					return Json(new { success = false, message = "Thông tin không hợp lệ" });
				}

				var cartItemDto = _mapper.Map<CartItemDto>(model);
				var isSucceeded = await _cartService.AddToCartAsync(cartItemDto);
				if (!isSucceeded)
				{
					_logger.LogWarning((string?)TempData["Message"]);
					return Json(new { success = false, message = TempData["Message"] });
				}

				var productCount = await _cartService.GetCartProductCountAsync();

				_logger.LogInformation((string?)TempData["Message"]);
				return Json(new { success = true, message = TempData["Message"], productCount });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return Json(new { success = false, message = "Có lỗi xảy ra" });
			}
		}

		public async Task<JsonResult> GetCartProductCount()
		{
			try
			{
				var productCount = await _cartService.GetCartProductCountAsync();

				return Json(new { productCount });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return Json(new { success = false });
			}
		}
	}
}
