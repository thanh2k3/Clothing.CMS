using AutoMapper;
using Clothing.CMS.Application.Services;
using Clothing.CMS.Application.Users.Dto;
using Clothing.CMS.Entities.Authorization.Users;
using Clothing.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Clothing.CMS.Application.Users
{
	public class UserService : BaseService, IUserService
	{
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly IHostingEnvironment _environment;

		public UserService(
			UserManager<User> userManager,
			IMapper mapper,
			IHostingEnvironment environment,
			IHttpContextAccessor httpContextAccessor,
			ITempDataDictionaryFactory tempDataDictionaryFactory) : base(httpContextAccessor, tempDataDictionaryFactory)
		{
			_userManager = userManager;
			_mapper = mapper;
			_environment = environment;
		}

		public async Task<ICollection<UserDto>> GetAll()
		{
			try
			{
				var users = await _userManager.Users.Where(x => !x.IsDeleted)
					.OrderByDescending(x => x.Id)
					.ToListAsync();
				var usersDto = _mapper.Map<ICollection<UserDto>>(users);

				return usersDto;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<bool> CreateAsync(CreateUserDto model, IFormFile? avatarURL)
		{
			try
			{
				var user = _mapper.Map<User>(model);
				var data = await _userManager.Users.FirstOrDefaultAsync(
					x => x.Email == user.Email && x.IsDeleted == false);
				if (data != null)
				{
					NotifyMsg("Người dùng đã tồn tại");
					return false;
				}

				if (avatarURL != null)
				{
					var imgName = "user-" + Guid.NewGuid().ToString() + "-" + avatarURL.FileName;
					var name = Path.Combine(_environment.WebRootPath + "/img/user", imgName);
					await avatarURL.CopyToAsync(new FileStream(name, FileMode.Create));
					user.AvatarURL = "/img/user/" + imgName;
				}
				else
				{
					user.AvatarURL = "/img/user/avatar-default.jpg";
				}

				FillUserAuthInfo(user);
				IdentityResult result = await _userManager.CreateAsync(user, model.Password);
				if (!result.Succeeded)
				{
					NotifyMsg("Thêm mới người dùng thất bại");
					return false;
				}

				NotifyMsg("Thêm mới người dùng thành công");
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception();
			}
		}

		public async Task<EditUserDto> GetById(int id)
		{
			try
			{
				var user = await _userManager.FindByIdAsync(id.ToString());
				if (user == null)
				{
					throw new KeyNotFoundException($"Không tìm thấy người dùng với ID: \"{id}\"");
				}

				var userDto = _mapper.Map<EditUserDto>(user);

				return userDto;
			}
			catch (KeyNotFoundException ex)
			{
				throw new Exception(ex.Message);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<bool> UpdateAsync(EditUserDto model, IFormFile? avatarURL)
		{
			try
			{
				var existsUser = await _userManager.Users.AnyAsync(
					x => x.Email == model.Email && x.Id != model.Id && x.IsDeleted == false);
				if (existsUser)
				{
					NotifyMsg("Người dùng đã tồn tại");
					return false;
				}

				var user = await _userManager.FindByIdAsync(model.Id.ToString());
				if (user == null)
				{
					NotifyMsg("Không tìm thấy người dùng");
					return false;
				}

				if (avatarURL != null)
				{
					var imgName = "user-" + Guid.NewGuid().ToString() + "-" + avatarURL.FileName;
					var name = Path.Combine(_environment.WebRootPath + "/img/user", imgName);
					await avatarURL.CopyToAsync(new FileStream(name, FileMode.Create));
					user.AvatarURL = "/img/user/" + imgName;
				}

				user.Email = model.Email;
				user.UserName = model.Email;
				user.FirstName = model.FirstName;
				user.LastName = model.LastName;
				FillUserAuthInfo(user);

				IdentityResult result = await _userManager.UpdateAsync(user);
				if (!result.Succeeded)
				{
					NotifyMsg("Cập nhật người dùng thất bại");
					return false;
				}

				NotifyMsg("Cập nhật người dùng thành công");
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception();
			}
		}

		public async Task<bool> DeleteAsync(int id)
		{
			try
			{
				var user = await _userManager.FindByIdAsync(id.ToString());
				if (user == null)
				{
					NotifyMsg("Không tìm thấy người dùng");
					return false;
				}

				user.IsDeleted = true;
				FillUserAuthInfo(user);

				IdentityResult result = await _userManager.UpdateAsync(user);
				if (!result.Succeeded)
				{
					NotifyMsg("Xóa người dùng thất bại");
					return false;
					
				}

				NotifyMsg("Xóa người dùng thành công");
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<List<SelectListItem>> GetSelectListItemAsync()
		{
			try
			{
				var result = _userManager.Users.Where(x => x.IsDeleted == false)
					.Select(x => new SelectListItem()
					{
						Value = x.Id.ToString(),
						Text = x.Email,
					})
					.ToList();

				return result;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
