using AutoMapper;
using Clothing.CMS.Application.Common;
using Clothing.CMS.Application.Services;
using Clothing.CMS.Application.Users.Dto;
using Clothing.CMS.Entities.Authorization.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Clothing.CMS.Application.Users
{
	public class UserService : BaseService, IUserService
	{
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public UserService(
			UserManager<User> userManager,
			IMapper mapper,
			IHttpContextAccessor httpContextAccessor,
			ITempDataDictionaryFactory tempDataDictionaryFactory) : base(httpContextAccessor, tempDataDictionaryFactory)
		{
			_userManager = userManager;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
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

		public async Task<bool> CreateAsync(CreateUserDto model)
		{
			try
			{
				var data = _mapper.Map<User>(model);
				var user = await _userManager.Users.FirstOrDefaultAsync(
					x => x.Email == data.Email && x.IsDeleted == false);
				if (user != null)
				{
					NotifyMsg("Người dùng đã tồn tại");
					return false;
				}

				FillUserAuthInfo(data);
				IdentityResult result = await _userManager.CreateAsync(data, model.Password);
				if (result.Succeeded)
				{
					NotifyMsg("Thêm mới người dùng thành công");
					return true;
				}

				NotifyMsg("Thêm mới người dùng thất bại");
				return false;
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

		public async Task<bool> UpdateAsync(EditUserDto model)
		{
			try
			{
				var user = _mapper.Map<User>(model);
				var existsUser = await _userManager.Users.AnyAsync(
					x => x.Email == user.Email &&
					x.Id != user.Id && x.IsDeleted == false);
				if (existsUser)
				{
					NotifyMsg($"Người dùng đã tồn tại");
					return false;
				}

				var data = await _userManager.FindByIdAsync(user.Id.ToString());
				if (data == null)
				{
					NotifyMsg("Không tìm thấy người dùng");
					return false;
				}

				data.Email = user.Email;
				data.UserName = user.Email;
				data.FirstName = user.FirstName;
				data.LastName = user.LastName;
				data.AvatarURL = user.AvatarURL;
				FillUserAuthInfo(data);

				IdentityResult result = await _userManager.UpdateAsync(data);
				if (result.Succeeded)
				{
					NotifyMsg("Cập nhật người dùng thành công");
					return true;
				}

				NotifyMsg("Cập nhật người dùng thất bại");
				return false;
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
				if (result.Succeeded)
				{
					NotifyMsg("Xóa người dùng thành công");
					return true;
				}

				NotifyMsg("Xóa người dùng thất bại");
				return false;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		protected void FillUserAuthInfo(User user)
		{
			if (user != null)
			{
				var userId = _httpContextAccessor.HttpContext.User.GetUserProperty(CustomClaimTypes.NameIdentifier);
				var timeNow = DateTime.Now;
				if (user.Id < 1)
				{
					user.CreatedBy = userId;
					user.CreatedTime = timeNow;
					user.ModifiedBy = userId;
					user.ModifiedTime = timeNow;
				}
				else
				{
					if (string.IsNullOrEmpty(user.CreatedBy))
					{
						user.CreatedBy = userId;
						user.CreatedTime = timeNow;
					}
					user.ModifiedBy = userId;
					user.ModifiedTime = timeNow;
				}
			}
		}
	}
}
