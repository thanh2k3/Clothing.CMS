using AutoMapper;
using Clothing.CMS.Application.Categories;
using Clothing.CMS.Application.Categories.Dto;
using Clothing.CMS.Web.Areas.Admin.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.Controllers.Manage
{
	[Authorize]
	public class CategoryController : BaseController
	{
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;
		private readonly ILogger<CategoryController> _logger;

		public CategoryController(ICategoryService categoryService,
			IMapper mapper,
			ILogger<CategoryController> logger)
		{
			_categoryService = categoryService;
			_mapper = mapper;
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<JsonResult> GetData()
		{
			try
			{
				var cateDto = await _categoryService.GetAll();
				var cateVM = _mapper.Map<ICollection<CategoryViewModel>>(cateDto);

				return Json(cateVM);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);

				return Json(null);
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(CreateCategoryViewModel model)
		{
			if (ModelState.IsValid)
			{
				var cateVM = _mapper.Map<CreateCategoryDto>(model);
				var isSucceeded = await _categoryService.CreateAsync(cateVM);

				if (isSucceeded)
				{
					return Json(new { success = true, message = TempData["Message"] });
				}

				return Json(new { success = false, message = TempData["Message"] });
			}

			return Json(new { success = false, message = "Thông tin không hợp lệ" });
		}

		public async Task<IActionResult> EditModal(int id)
		{
			var cateDto = await _categoryService.GetById(id);
			var cateVM = _mapper.Map<EditCategoryViewModel>(cateDto);

			return PartialView("_EditModal", cateVM);
		}

		[HttpPost]
        public async Task<IActionResult> Edit(EditCategoryViewModel model)
        {
			try
			{
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Thông tin không hợp lệ" });
                }

                var cateDto = _mapper.Map<EditCategoryDto>(model);
                var isSucceeded = await _categoryService.UpdateAsync(cateDto);

                if (!isSucceeded)
                {
                    return Json(new { success = false, message = TempData["Message"] });
                }

                return Json(new { success = true, message = TempData["Message"] });
            }
			catch (Exception ex)
			{
                return Json(new { success = false, message = ex.Message });
            }
        }
	}
}
