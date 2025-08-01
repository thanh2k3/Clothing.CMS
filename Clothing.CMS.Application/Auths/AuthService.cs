using Clothing.CMS.Application.Auths.Dto;
using Clothing.CMS.Application.Common.Dto;
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

		public async Task<BaseResponse<bool>> LoginAsync(LoginDto model)
		{
			try
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user == null)
				{
					return BaseResponse<bool>.Fail("Email không chính xác.");
				}

				if (!await _userManager.CheckPasswordAsync(user, model.Password))
				{
					return BaseResponse<bool>.Fail("Mật khẩu không chính xác.");
				}

				var result = await _signManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: true);
				if (result.Succeeded)
				{
					var userRoles = await _userManager.GetRolesAsync(user);

					// Create list claim
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

					// Create identity + principal
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

					return BaseResponse<bool>.Ok(true, "Đăng nhập thành công.");
				}
				else if (result.IsLockedOut)
				{
					return BaseResponse<bool>.Fail("Tài khoản đã bị khóa do nhập sai quá nhiều lần. Vui lòng thử lại sau.");
				}
				else
				{
					return BaseResponse<bool>.Fail("Đăng nhập không thành công.");
				}
			}
			catch (Exception ex)
			{
				return BaseResponse<bool>.Fail("Có lỗi xảy ra trong quá trình đăng nhập.");
			}
		}

		public async Task LogoutAsync()
		{
			await _signManager.SignOutAsync();
		}
	}
}
