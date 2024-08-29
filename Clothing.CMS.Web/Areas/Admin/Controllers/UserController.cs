using AutoMapper;
using Clothing.CMS.Application.Users;
using Clothing.CMS.Application.Users.Dto;
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

        public UserController(IMapper mapper, IUserService userService)
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

        public async Task<IActionResult> EditModal(int id)
        {
            var userdto = await _userService.GetById(id);
            var userVM = _mapper.Map<EditUserViewModel>(userdto);

            return PartialView("_EditModal", userVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var userDto = _mapper.Map<EditUserDto>(model);
                var isSucceeded = await _userService.UpdateAsync(userDto);
                if (isSucceeded) {
                    return Json(new { success = true, message = "Chỉnh sửa người dùng thành công" });
                }

                return Json(new { success = false, message = "Chỉnh sửa người dùng thất bại" });
			}

            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var isSucceeded = await _userService.DeleteAsync(id);

                if (isSucceeded)
                {
                    return Json(new { success = true });
                }

				return Json(new { success = false });
			}

			return Json(new { success = false });
		}
	}
}
