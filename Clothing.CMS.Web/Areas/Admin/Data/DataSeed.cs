using Clothing.CMS.Application.Common;
using Clothing.CMS.Entities.Authorization.Roles;
using Clothing.CMS.Entities.Authorization.Users;
using Clothing.CMS.EntityFrameworkCore.Pattern;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Clothing.CMS.Web.Areas.Admin.Data
{
	public static class DataSeed
	{
		public static async Task Seed(IServiceProvider serviceProvider)
		{
			IServiceScopeFactory scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

			using (IServiceScope scope = scopeFactory.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<CMSDbContext>();
				context.Database.Migrate();

				UserManager<User> _userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
				RoleManager<Role> _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

				// Seed database code goes here

				// User Info
				string firstName = "Super";
				string lastName = "Admin";
				string email = "admin@gmail.com";
				string password = "Admin@123456";
				string role = "SuperAdmin";
				string role2 = "SeniorManager";
				string role3 = "Manager";

				if (await _userManager.FindByNameAsync(email) == null)
				{
					// Create SuperAdmins role if it doesn't exist
					if (await _roleManager.FindByNameAsync(role) == null)
					{
						await _roleManager.CreateAsync(new Role { Name = role });
					}
					if (await _roleManager.FindByNameAsync(role2) == null)
					{
                        await _roleManager.CreateAsync(new Role { Name = role2 });

                    }
                    if (await _roleManager.FindByNameAsync(role3) == null)
					{
                        await _roleManager.CreateAsync(new Role { Name = role3 });
                    }

                    // Create user account if it doesn't exist
                    User user = new User
					{
						UserName = email,
						Email = email,
						//extended properties
						FirstName = firstName,
						LastName = lastName,
						AvatarURL = "/img/avatar-default.png",
						DateRegistered = DateTime.UtcNow.ToString(),
						Position = "",
						NickName = "",
					};

					IdentityResult result = await _userManager.CreateAsync(user, password);

					if (result.Succeeded)
					{
						await _userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.GivenName, user.FirstName));
						await _userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Surname, user.LastName));
						await _userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.AvatarURL, user.AvatarURL));

						await _userManager.AddToRoleAsync(user, role);
					}
				}
			}
		}
	}
}
