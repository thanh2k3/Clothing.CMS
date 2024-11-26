using AutoMapper;
using Clothing.CMS.Entities.Authorization.Roles;

namespace Clothing.CMS.Application.Roles.Dto
{
	public class RoleMapProfile : Profile
	{
		public RoleMapProfile()
		{
			CreateMap<Role, RoleDto>();
			CreateMap<CreateRoleDto, Role>();
			CreateMap<Role, EditRoleDto>();
			CreateMap<EditRoleDto, Role>();
		}
	}
}
