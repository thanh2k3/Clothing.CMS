﻿using Clothing.CMS.Entities;
using Clothing.CMS.Entities.Authorization.Roles;
using Microsoft.EntityFrameworkCore;

namespace Clothing.CMS.EntityFrameworkCore.Pattern
{
	public class CMSModuleBuilder
	{
		public static void RegisterModule(ModelBuilder builder)
		{
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
		}
	}
}
