using Clothing.CMS.Application.Roles.Dto;

namespace Clothing.CMS.Application.Roles
{
	public interface IRoleService
	{
		Task<ICollection<RoleDto>> GetAll();
		Task<bool> CreateAsync(CreateRoleDto model);
	}
}
