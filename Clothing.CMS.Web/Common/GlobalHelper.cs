using Clothing.CMS.Application.Users;
using Clothing.CMS.EntityFrameworkCore.Pattern.Repositories;

namespace Clothing.CMS.Web.Common
{
    public static class GlobalHelper
    {
        public static void RegisterAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserService));
        }

        public static void RegisterServiceLifetimer(IServiceCollection services)
        {
            //Scoped services
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserService, UserService>();
        }
    }
}
