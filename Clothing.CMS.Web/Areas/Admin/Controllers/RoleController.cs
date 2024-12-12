using AutoMapper;
using Clothing.CMS.Application.Roles;
using Clothing.CMS.Application.Roles.Dto;
using Clothing.CMS.Web.Areas.Admin.ViewModels.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public RoleController(IRoleService roleService, IMapper mapper, ILoggerFactory loggerFactory)
        {
            _roleService = roleService;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<RoleController>();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetData()
        {
            try
            {
                var roleDto = await _roleService.GetAll();
                var roleVM = _mapper.Map<ICollection<RoleViewModel>>(roleDto);

                _logger.LogInformation("Lấy ra tất cả quyền");
                return Json(roleVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new { success = false, message = "Có lỗi xảy ra" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Create(CreateRoleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
					var roleDto = _mapper.Map<CreateRoleDto>(model);
					var isSucceeded = await _roleService.CreateAsync(roleDto);
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
				var roleDto = await _roleService.GetById(id);
				var roleVM = _mapper.Map<EditRoleViewModel>(roleDto);

				_logger.LogInformation($"lấy ra quyền với ID: {id}");
				return PartialView("_EditModal", roleVM);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return PartialView("_EditModal");
			}
		}

		[HttpPost]
		public async Task<JsonResult> Edit(EditRoleViewModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var roleDto = _mapper.Map<EditRoleDto>(model);
					var isSucceeded = await _roleService.UpdateAsync(roleDto);
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

		[HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
				var isSucceeded = await _roleService.DeleteAsync(id);
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
