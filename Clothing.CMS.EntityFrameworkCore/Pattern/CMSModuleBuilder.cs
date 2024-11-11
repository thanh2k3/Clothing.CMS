using Clothing.CMS.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clothing.CMS.EntityFrameworkCore.Pattern
{
	public class CMSModuleBuilder
	{
		public static void RegisterModule(ModelBuilder builder)
		{
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
        }
	}
}
