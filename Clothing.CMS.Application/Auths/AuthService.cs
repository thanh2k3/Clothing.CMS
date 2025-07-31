using Clothing.CMS.Application.Auths.Dto;
using Clothing.CMS.Application.Services;
using Clothing.CMS.Entities.Authorization.Roles;
using Clothing.CMS.Entities.Authorization.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Claims;

namespace Clothing.CMS.Application.Auths
{
	public class AuthService : BaseService, IAuthService
	{
		private readonly SignInManager<User> _signManager;
		private readonly UserManager<User> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AuthService(
			SignInManager<User> signManager,
			UserManager<User> userManager,
			IHttpContextAccessor httpContextAccessor,
			ITempDataDictionaryFactory tempDataDictionaryFactory) : base(httpContextAccessor, tempDataDictionaryFactory)
		{
			_signManager = signManager;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<bool> LoginAsync(LoginDto model)
		{
			try
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user == null)
				{
					NotifyMsg("Email không chính xác.");
					return false;
				}

				if (!await _userManager.CheckPasswordAsync(user, model.Password))
				{
					NotifyMsg("Mật khẩu không chính xác.");
					return false;
				}

				var result = await _signManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: true);
				if (result.Succeeded)
				{
					var userRoles = await _userManager.GetRolesAsync(user);

					// Tạo danh sách claim
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, user.UserName),
						new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
						new Claim(ClaimTypes.Email, user.Email),
						new Claim("SecurityStamp", user.SecurityStamp)
					};

					foreach (var userRole in userRoles)
					{
						claims.Add(new Claim(ClaimTypes.Role, userRole));
					}

					// Tạo identity + principal
					var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
					var principal = new ClaimsPrincipal(identity);

					await _httpContextAccessor.HttpContext.SignInAsync(
						IdentityConstants.ApplicationScheme,
						principal,
						new AuthenticationProperties
						{
							IsPersistent = model.RememberMe,
							ExpiresUtc = DateTime.UtcNow.AddHours(1)
						});

					NotifyMsg("Đăng nhập thành công");
					return true;
				}
				else if (result.IsLockedOut)
				{
					NotifyMsg("Tài khoản đã bị khóa do nhập sai quá nhiều lần. Vui lòng thử lại sau.");
					return false;
				}
				else
				{
					NotifyMsg("Đăng nhập không thành công.");
					return false;
				}
			}
			catch (Exception ex)
			{
				NotifyMsg("Có lỗi xảy ra trong quá trình đăng nhập.");
				return false;
			}
		}

		public async Task LogoutAsync()
		{
			await _signManager.SignOutAsync();
		}
	}
}
