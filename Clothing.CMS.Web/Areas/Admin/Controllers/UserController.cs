using AutoMapper;
using Clothing.CMS.Application.Users;
using Clothing.CMS.Application.Users.Dto;
using Clothing.CMS.EntityFrameworkCore.Pattern;
using Clothing.CMS.Web.Areas.Admin.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IMapper mapper, IUserService userService, CMSDbContext context)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

		public async Task<JsonResult> GetData()
		{
			var userDto = await _userService.GetAll();
            var userVM = _mapper.Map<IEnumerable<UserViewModel>>(userDto);

            return Json(userVM);
		}

        [HttpPost]
        public async Task<JsonResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userVM = _mapper.Map<CreateUserDto>(model);
                var isSucceeded = await _userService.CreateAsync(userVM);
                if (isSucceeded)
                {
                    return Json(new { success  = true, message = "Thêm mới người dùng thành công" });
                }

				return Json(new { success = false, message = "Người dùng này đã tồn tại!" });
			}

			return Json(new { success = false, message = "Lỗi" });
		}
	}
}
