using Clothing.CMS.Application.Common.Dto;
using Clothing.CMS.Application.Roles.Dto;

namespace Clothing.CMS.Application.Roles
{
	public interface IRoleService
	{
		Task<PagedResponseDto<List<RoleDto>>> GetAllPaging(RolePagedRequestDto input);
		Task<EditRoleDto> GetById(int id);
		Task<bool> CreateAsync(CreateRoleDto model);
		Task<bool> UpdateAsync(EditRoleDto model);
		Task<bool> DeleteAsync(int id);
	}
}
