using Clothing.CMS.Entities.Authorization.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Clothing.CMS.Web.Common
{
	public class CustomRoleValidator : RoleValidator<Role>
	{
		public override async Task<IdentityResult> ValidateAsync(RoleManager<Role> manager, Role role)
		{
			var roleName = await manager.GetRoleNameAsync(role);
			if (string.IsNullOrWhiteSpace(roleName))
			{
				return IdentityResult.Failed(new IdentityError
				{
					Code = "RoleNameIsNotValid",
					Description = "Role Name is not valid!"
				});
			}
			else
			{
				// Id: Create, NormalizedName: Update, IsDeleted: Delete
				var owner = await manager.Roles.FirstOrDefaultAsync(x => x.Id == role.Id
																	&& x.NormalizedName == roleName
																	&& x.IsDeleted == true);

				if (owner != null && !string.Equals(manager.GetRoleIdAsync(owner), manager.GetRoleIdAsync(role)))
				{
					return IdentityResult.Failed(new IdentityError
					{
						Code = "DuplicateRoleName",
						Description = "this role already exist in this App!"
					});
				}
			}

			return IdentityResult.Success;
		}
	}
}
