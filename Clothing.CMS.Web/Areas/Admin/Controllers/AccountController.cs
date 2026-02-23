using AutoMapper;
using Clothing.CMS.Application.Auths;
using Clothing.CMS.Application.Auths.Dto;
using Clothing.CMS.Entities.Authorization.Users;
using Clothing.CMS.Web.Areas.Admin.ViewModels.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clothing.CMS.Web.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthService _authService;
		private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<User> userManager,
			SignInManager<User> signInManager,
			IAuthService authService,
			IMapper mapper,
			ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
			_mapper = mapper;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToLocal("/admin/home");

            if (returnUrl == null)
                returnUrl = "/admin/home";

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
			}

			var loginDto = _mapper.Map<LoginDto>(model);
			var response = await _authService.LoginAsync(loginDto);

			if (response.Success)
			{
				_logger.LogInformation(response.Message);
				return Json(new { success = true, message = response.Message });
			}
			else
			{
				_logger.LogWarning(response.Message);
				return Json(new { success = false, message = response.Message, errors = response.Errors });
			}
		}

		[HttpPost]
        public async Task<IActionResult> Logout()
        {
            var email = User.Identity.IsAuthenticated
                ? User.FindFirst(ClaimTypes.Email)?.Value ?? User.Identity.Name
                : "Unknown";

			await _authService.LogoutAsync();

            _logger.LogInformation($"Người dùng {email} đã đăng xuất.");

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

		#region Helpers

		private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

		#endregion
	}
}
