using AutoMapper;
using Clothing.CMS.Application.Products;
using Clothing.CMS.Web.Areas.Admin.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.Controllers
{
	[Authorize]
	public class ProductController : BaseController
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

		public IActionResult Index()
		{
			return View();
		}

		public async Task<JsonResult> GetData()
		{
			try
			{
				var productDto = await _productService.GetAll();
				var productVM = _mapper.Map<ICollection<ProductViewModel>>(productDto);

				_logger.LogInformation("Lấy ra tất cả sản phẩm");
				return Json(productVM);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return Json(null);
			}
		}
	}
}
