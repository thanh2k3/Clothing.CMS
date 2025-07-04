﻿using AutoMapper;
using Clothing.CMS.Application.Common;
using Clothing.CMS.Application.Common.Dto;
using Clothing.CMS.Application.Roles.Dto;
using Clothing.CMS.Application.Services;
using Clothing.CMS.Entities.Authorization.Roles;
using Clothing.CMS.EntityFrameworkCore.Pattern;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace Clothing.CMS.Application.Roles
{
	public class RoleService : BaseService, IRoleService
	{
		private readonly CMSDbContext _context;
		private readonly RoleManager<Role> _roleManager;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public RoleService(
			CMSDbContext context,
			RoleManager<Role> roleManager,
			IMapper mapper,
			IHttpContextAccessor httpContextAccessor,
			ITempDataDictionaryFactory tempDataDictionaryFactory) : base(httpContextAccessor, tempDataDictionaryFactory)
		{
			_context = context;
			_roleManager = roleManager;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<PagedResponseDto<List<RoleDto>>> GetAllPaging(RolePagedRequestDto input)
		{
			try
			{
				var query = _context.Set<Role>()
					.Where(x => string.IsNullOrEmpty(input.Keyword) || x.Name.Contains(input.Keyword));

				var list = await query.Skip(input.SkipCount).Take(input.PageSize)
					.Where(x => !x.IsDeleted)
					.OrderByDescending(x => x.Id)
					.ToListAsync();

				var totalCount = await query.CountAsync();
				var data = _mapper.Map<List<RoleDto>>(list);
				var result = new PagedResponseDto<List<RoleDto>>(data, input.PageNumber, input.PageSize, totalCount, input.Keyword);

				return result;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<bool> CreateAsync(CreateRoleDto model)
		{
			try
			{
				var data = _mapper.Map<Role>(model);
				var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == data.Name && x.IsDeleted == false);
				if (role != null)
				{
					NotifyMsg("Quyền đã tồn tại");
					return false;
				}

				FillRoleAuthInfo(data);
				IdentityResult result = await _roleManager.CreateAsync(data);
				if (result.Succeeded)
				{
					NotifyMsg("Thêm mới quyền thành công");
					return true;
				}

				NotifyMsg("Thêm mới quyền thất bại");
				return false;
			}
			catch (Exception ex)
			{
				throw new Exception();
			}
		}

		public async Task<EditRoleDto> GetById(int id)
		{
			try
			{
				var role = await _roleManager.FindByIdAsync(id.ToString());
				if (role == null)
				{
					throw new KeyNotFoundException($"Không tìm thấy quyền với ID: \"{id}\"");
				}

				var roleDto = _mapper.Map<EditRoleDto>(role);

				return roleDto;
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

		public async Task<bool> UpdateAsync(EditRoleDto model)
		{
			try
			{
				var role = _mapper.Map<Role>(model);
				var existsRole = await _roleManager.Roles
					.AnyAsync(x => x.Name == role.Name && x.Id != role.Id && x.IsDeleted == false);
				if (existsRole)
				{
					NotifyMsg("Quyền đã tồn tại");
					return false;
				}

				var data = await _roleManager.FindByIdAsync(role.Id.ToString());
				if (data == null)
				{
					NotifyMsg("Không tìm thấy quyền");
					return false;
				}

				data.Name = role.Name;
				data.Description = role.Description;
				FillRoleAuthInfo(data);

				IdentityResult result = await _roleManager.UpdateAsync(data);
				if (result.Succeeded)
				{
					NotifyMsg("Cập nhật quyền thành công");
					return true;
				}

				NotifyMsg("Cập nhật quyền thất bại");
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
				var role = await _roleManager.FindByIdAsync(id.ToString());
				if (role == null)
				{
					NotifyMsg("Không tìm thấy quyền");
					return false;
				}

				role.IsDeleted = true;
				FillRoleAuthInfo(role);

				IdentityResult result = await _roleManager.UpdateAsync(role);
				if (result.Succeeded)
				{
					NotifyMsg("Xóa quyền thành công");
					return true;
				}

				NotifyMsg("Xóa quyền thất bại");
				return false;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		protected void FillRoleAuthInfo(Role role)
		{
			if (role != null)
			{
				var userId = _httpContextAccessor.HttpContext.User.GetUserProperty(CustomClaimTypes.NameIdentifier);
				var timeNow = DateTime.Now;
				if (role.Id < 1)
				{
					role.CreatedBy = userId;
					role.CreatedTime = timeNow;
					role.ModifiedBy = userId;
					role.ModifiedTime = timeNow;
				}
				else
				{
					if (string.IsNullOrEmpty(role.CreatedBy))
					{
						role.CreatedBy = userId;
						role.CreatedTime = timeNow;
					}
					role.ModifiedBy = userId;
					role.ModifiedTime = timeNow;
				}
			}
		}
	}
}
