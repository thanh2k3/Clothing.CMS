using Clothing.CMS.Application.Roles.Dto;

namespace Clothing.CMS.Application.Roles
{
	public interface IRoleService
	{
		Task<ICollection<RoleDto>> GetAll();
		Task<EditRoleDto> GetById(int id);
		Task<bool> CreateAsync(CreateRoleDto model);
		Task<bool> UpdateAsync(EditRoleDto model);
	}
}
