using AutoMapper;
using Clothing.CMS.Application.Roles;
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
                return Json(new { success = false, message = "Có lỗi xảy ra khi tải dữ liệu" });
            }
        }
    }
}
