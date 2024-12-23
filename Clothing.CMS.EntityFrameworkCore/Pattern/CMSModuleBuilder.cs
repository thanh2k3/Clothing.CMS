using Clothing.CMS.Entities;
using Clothing.CMS.Entities.Authorization.Roles;
using Clothing.CMS.Entities.Authorization.Users;
using Microsoft.EntityFrameworkCore;

namespace Clothing.CMS.EntityFrameworkCore.Pattern
{
	public class CMSModuleBuilder
	{
		public static void RegisterModule(ModelBuilder builder)
		{
			// Custom User
			builder.Entity<User>()
				.HasIndex(u => u.NormalizedUserName)
				.HasDatabaseName("UserNameIndex")
				.IsUnique(false); // Loại bỏ Unique Constraint

			// Custom Role
			builder.Entity<Role>()
				.HasIndex(r => r.NormalizedName)
				.HasDatabaseName("RoleNameIndex")
				.IsUnique(false); // Loại bỏ Unique Constraint

			// LogEvent
			builder.Entity<LogEvent>(entity =>
			{
				entity.HasKey(t => t.Id);
			});

            // Category
            builder.Entity<Category>(entity =>
            {
                entity.HasKey(t => t.Id);
            });

			// Product
			builder.Entity<Product>(entity =>
			{
				entity.HasKey(t => t.Id);
			});

			// Order
			builder.Entity<Order>(entity =>
			{
				entity.HasKey(t => t.Id);
			});

			// OrderProduct
			builder.Entity<OrderProduct>(entity =>
			{
				entity.HasKey(t => t.Id);

				entity.HasOne(t => t.Order)
					.WithMany(t => t.OrderProducts)
					.OnDelete(DeleteBehavior.Cascade);
			});
		}
	}
}
