using AutoMapper;
using Clothing.CMS.Application.Categories;
using Clothing.CMS.Application.Products;
using Clothing.CMS.Application.Products.Dto;
using Clothing.CMS.Web.Areas.Admin.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clothing.CMS.Web.Areas.Admin.Controllers
{
	[Authorize]
	public class ProductController : BaseController
	{
		private readonly IProductService _productService;
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;
		public static List<SelectListItem> CategoryItems;

		public ProductController(IProductService productService, ICategoryService categoryService, IMapper mapper, ILoggerFactory loggerFactory)
		{
			_productService = productService;
			_categoryService = categoryService;
			_mapper = mapper;
			_logger = loggerFactory.CreateLogger<ProductController>();
		}

		public async Task<IActionResult> Index()
		{
			CategoryItems = await _categoryService.GetSelectListItemAsync();
			ViewBag.CategoryItems = CategoryItems;

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

		[HttpPost]
		public async Task<JsonResult> Create(CreateProductViewModel model, IFormFile? image)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var productDto = _mapper.Map<CreateProductDto>(model);
					var isSucceeded = await _productService.CreateAsync(productDto, image);

					if (isSucceeded)
					{
						_logger.LogInformation((string?)TempData["Message"]);
						return Json(new { success = true, message = TempData["Message"] });
					}

					_logger.LogWarning((string?)TempData["Message"]);
					return Json(new { success = false, message = TempData["Message"] });
				}

				_logger.LogWarning("Thông tin không hợp lệ");
				return Json(new { success = false, message = "Thông tin không hợp lệ" });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return Json(new { success = false, message = "Có lỗi xảy ra" });
			}
		}

		public async Task<ActionResult> EditModal(int id)
        {
            try
            {
                var productDto = await _productService.GetById(id);
                var productVM = _mapper.Map<EditProductViewModel>(productDto);

				ViewBag.CategoryItems = CategoryItems;

				return PartialView("_EditModal", productVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return PartialView("_EditModal");
            }
        }

		[HttpPost]
		public async Task<JsonResult> Edit(EditProductViewModel model, IFormFile? image)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var productDto = _mapper.Map<EditProductDto>(model);
					var isSucceeded = await _productService.UpdateAsync(productDto, image);

					if (isSucceeded)
					{
						_logger.LogInformation((string?)TempData["Message"]);
						return Json(new { success = true, message = TempData["Message"] });
					}

					_logger.LogWarning((string?)TempData["Message"]);
					return Json(new { success = false, message = TempData["Message"] });
				}

				_logger.LogWarning("Thông tin không hợp lệ");
				return Json(new { success = false, message = "Thông tin không hợp lệ" });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return Json(new { success = false, message = "Có lỗi xảy ra" });
			}
		}

		public async Task<ActionResult> ViewModal(int id)
		{
			try
			{
				var productDto = await _productService.GetByIdIncluding(id);
				var productVM = _mapper.Map<ProductViewModel>(productDto);
				return PartialView("_ViewModal", productVM);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return PartialView("_ViewModal");
			}
		}

		[HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var isSucceeded = await _productService.DeleteAsync(id);
                if (isSucceeded)
                {
                    _logger.LogInformation((string?)TempData["Message"]);
                    return Json(new { success = true, message = TempData["Message"] });
                }

                _logger.LogWarning((string?)TempData["Message"]);
                return Json(new { success = false, message = TempData["Message"] });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new { success = false, message = "Có lỗi xảy ra" });
            }
        }
	}
}
