﻿using Clothing.CMS.Application.Users.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clothing.CMS.Application.Users
{
    public interface IUserService
    {
        Task<ICollection<UserDto>> GetAll();
		Task<EditUserDto> GetById(int id);
        Task<bool> CreateAsync(CreateUserDto model, IFormFile? avatarURL);
		Task<bool> UpdateAsync(EditUserDto model, IFormFile? avatarURL);
        Task<bool> DeleteAsync(int id);
        Task<List<SelectListItem>> GetSelectListItemAsync();
	}
}
