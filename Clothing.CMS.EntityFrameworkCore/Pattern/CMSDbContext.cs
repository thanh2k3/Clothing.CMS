using Clothing.CMS.Entities.Authorization.Roles;
using Clothing.CMS.Entities.Authorization.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Clothing.CMS.EntityFrameworkCore.Pattern
{
	public class CMSDbContext : IdentityDbContext<User, Role, int>
	{
		public CMSDbContext() { }

		public CMSDbContext(DbContextOptions options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			//Các bảng mặc định của CMS
			CMSModuleBuilder.RegisterModule(builder);
		}
	}
}
