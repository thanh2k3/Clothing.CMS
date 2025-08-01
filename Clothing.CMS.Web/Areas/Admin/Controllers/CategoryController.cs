using AutoMapper;
using Clothing.CMS.Application.Categories.Dto;
using Clothing.CMS.Application.Categories;
using Clothing.CMS.Web.Areas.Admin.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.Controllers
{
	[Authorize]
	public class CategoryController : BaseController
	{
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;

		public CategoryController(ICategoryService categoryService, IMapper mapper, ILoggerFactory loggerFactory)
		{
			_categoryService = categoryService;
			_mapper = mapper;
			_logger = loggerFactory.CreateLogger<CategoryController>();
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<JsonResult> GetData()
		{
			try
			{
				var response = await _categoryService.GetAll();
				var cateVM = _mapper.Map<ICollection<CategoryViewModel>>(response.Data);

				_logger.LogInformation(response.Message);
				return Json(cateVM);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return Json(new { success = false, message = "Có lỗi xảy ra khi tải dữ liệu" });
			}
		}

		[HttpPost]
		public async Task<JsonResult> Create(CreateCategoryViewModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var cateVM = _mapper.Map<CreateCategoryDto>(model);
					var response = await _categoryService.CreateAsync(cateVM);

					if (response.Success)
					{
						_logger.LogInformation(response.Message);
						return Json(new { success = true, message = response.Message });
					}

					_logger.LogWarning(response.Message);
					return Json(new { success = false, message = response.Message });
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
				var response = await _categoryService.GetById(id);
				var cateVM = _mapper.Map<EditCategoryViewModel>(response.Data);

				return PartialView("_EditModal", cateVM);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return PartialView("_EditModal");
			}
		}

		[HttpPost]
		public async Task<JsonResult> Edit(EditCategoryViewModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var cateDto = _mapper.Map<EditCategoryDto>(model);
					var response = await _categoryService.UpdateAsync(cateDto);

					if (response.Success)
					{
						_logger.LogInformation(response.Message);
						return Json(new { success = true, message = response.Message });
					}

					_logger.LogWarning(response.Message);
					return Json(new { success = false, message = response.Message });
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

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var response = await _categoryService.DeleteAsync(id);
				if (response.Success)
				{
					_logger.LogInformation(response.Message);
					return Json(new { success = true, message = response.Message });
				}

				_logger.LogWarning(response.Message);
				return Json(new { success = false, message = response.Message });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return Json(new { success = false, message = "Có lỗi xảy ra" });
			}
		}
	}
}
