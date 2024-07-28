using Clothing.CMS.Application.Users;
using Clothing.CMS.Application.Users.Dto;
using Clothing.CMS.Entities.Authorization.Users;
using Clothing.CMS.EntityFrameworkCore.Pattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly CMSDbContext _context;

        public UserController(IUserService userService, CMSDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

		public IActionResult GetUsers()
		{
			var data = _context.Users.ToList();

			return new JsonResult(data);
		}
	}
}
