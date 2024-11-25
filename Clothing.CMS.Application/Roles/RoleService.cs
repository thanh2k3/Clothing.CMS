using AutoMapper;
using Clothing.CMS.Application.Roles.Dto;
using Clothing.CMS.Application.Services;
using Clothing.CMS.Entities.Authorization.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace Clothing.CMS.Application.Roles
{
	public class RoleService : BaseService, IRoleService
	{
		private readonly RoleManager<Role> _roleManager;
		private readonly IMapper _mapper;

		public RoleService(
			RoleManager<Role> roleManager,
			IMapper mapper,
			IHttpContextAccessor httpContextAccessor,
			ITempDataDictionaryFactory tempDataDictionaryFactory) : base(httpContextAccessor, tempDataDictionaryFactory)
		{
			_roleManager = roleManager;
			_mapper = mapper;
		}

		public async Task<ICollection<RoleDto>> GetAll()
		{
			try
			{
				var role = await _roleManager.Roles.OrderByDescending(x => x.Id).ToListAsync();
				var roleDto = _mapper.Map<ICollection<RoleDto>>(role);

				return roleDto;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
