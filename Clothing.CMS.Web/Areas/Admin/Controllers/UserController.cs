using Clothing.CMS.Application.Users;
using Clothing.CMS.Application.Users.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await _userService.GetAllPaging(new UserPagedRequestDto());

                return View(data);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
