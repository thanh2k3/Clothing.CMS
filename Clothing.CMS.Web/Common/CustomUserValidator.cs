using Clothing.CMS.Entities.Authorization.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Clothing.CMS.Web.Common
{
	public class CustomUserValidator : UserValidator<User>
	{
		public override async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
		{
			var userName = await manager.GetUserNameAsync(user);
			if (string.IsNullOrEmpty(userName))
			{
				return IdentityResult.Failed(new IdentityError
				{
					Code = "UserNameIsNotValid",
					Description = "User Name is not valid!"
				});
			}
			else
			{
				// Id: Create, NormalizedUserName: Update, IsDeleted: Delete
				var owner = await manager.Users.FirstOrDefaultAsync(
					x => x.Id == user.Id && x.NormalizedUserName == userName && x.IsDeleted == true);
				if (owner != null && !string.Equals(manager.GetUserIdAsync(owner), manager.GetUserIdAsync(user)))
				{
					return IdentityResult.Failed(new IdentityError
					{
						Code = "DuplicateUserName",
						Description = "this user already exist in this App!"
					});
				}
			}

			return IdentityResult.Success;
		}
	}
}
