using AutoMapper;
using Clothing.CMS.Application.Categories;
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
    }
}
