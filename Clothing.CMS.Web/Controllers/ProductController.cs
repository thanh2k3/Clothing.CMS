using AutoMapper;
using Clothing.CMS.Application.Products;
using Clothing.CMS.Web.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService _productService;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;
		public ProductController(IProductService productService, IMapper mapper, ILoggerFactory loggerFactory)
		{
			_productService = productService;
			_mapper = mapper;
			_logger = loggerFactory.CreateLogger<ProductController>();
		}

		public async Task<IActionResult> Index()
		{
			try
			{
				var productDto = await _productService.GetAll();
				var productVM = _mapper.Map<ICollection<ProductViewModel>>(productDto);

				_logger.LogInformation("Lấy ra tất cả sản phẩm");
				return View(productVM);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return View();
			}
		}

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
	}
}
