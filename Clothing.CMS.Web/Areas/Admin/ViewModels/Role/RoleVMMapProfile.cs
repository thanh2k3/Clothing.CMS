using AutoMapper;
using Clothing.CMS.Application.Roles.Dto;

namespace Clothing.CMS.Web.Areas.Admin.ViewModels.Role
{
	public class RoleVMMapProfile : Profile
	{
		public RoleVMMapProfile()
		{
			CreateMap<RoleDto, RoleViewModel>();
			CreateMap<CreateRoleViewModel, CreateRoleDto>();
		}
	}
}
