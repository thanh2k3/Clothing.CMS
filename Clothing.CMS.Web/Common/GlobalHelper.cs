using Clothing.CMS.Application.Categories;
using Clothing.CMS.Application.LogEvents;
using Clothing.CMS.Application.Users;
using Clothing.CMS.EntityFrameworkCore.Pattern.Repositories;
using Clothing.CMS.Web.Areas.Admin.Controllers;
using Clothing.CMS.Web.Areas.Admin.Controllers.Manage;

namespace Clothing.CMS.Web.Common
{
    public static class GlobalHelper
    {
        public static void RegisterAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserService));
            services.AddAutoMapper(typeof(UserController));

            services.AddAutoMapper(typeof(LogEventService));
            services.AddAutoMapper(typeof(LogEventController));

            services.AddAutoMapper(typeof(CategoryService));
            services.AddAutoMapper(typeof(CategoryController));
        }

        public static void RegisterServiceLifetimer(IServiceCollection services)
        {
            //Scoped services
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILogEventService, LogEventService>();
            services.AddScoped<ICategoryService, CategoryService>();
        }
    }
}
