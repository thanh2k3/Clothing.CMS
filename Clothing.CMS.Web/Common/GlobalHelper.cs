using Clothing.CMS.Application.Carts;
using Clothing.CMS.Application.Categories;
using Clothing.CMS.Application.LogEvents;
using Clothing.CMS.Application.Orders;
using Clothing.CMS.Application.Products;
using Clothing.CMS.Application.Roles;
using Clothing.CMS.Application.Users;
using Clothing.CMS.EntityFrameworkCore.Pattern.Repositories;
using Clothing.CMS.Web.Areas.Admin.Controllers;
using Clothing.CMS.Web.Controllers;

namespace Clothing.CMS.Web.Common
{
    public static class GlobalHelper
    {
        public static void RegisterAutoMapper(IServiceCollection services)
        {
            // Admin
            services.AddAutoMapper(typeof(UserService));
            services.AddAutoMapper(typeof(UserController));

			services.AddAutoMapper(typeof(RoleService));
			services.AddAutoMapper(typeof(RoleController));

			services.AddAutoMapper(typeof(LogEventService));
            services.AddAutoMapper(typeof(LogEventController));

            services.AddAutoMapper(typeof(CategoryService));
            services.AddAutoMapper(typeof(CategoryController));

			services.AddAutoMapper(typeof(ProductService));
			services.AddAutoMapper(typeof(Areas.Admin.Controllers.ProductController));

			services.AddAutoMapper(typeof(OrderService));
			services.AddAutoMapper(typeof(OrderController));

            // Client
			services.AddAutoMapper(typeof(CartService));
			services.AddAutoMapper(typeof(CartController));

            services.AddAutoMapper(typeof(Controllers.ProductController));

            services.AddAutoMapper(typeof(CheckoutController));
		}

        public static void RegisterServiceLifetimer(IServiceCollection services)
        {
            //Scoped services
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ILogEventService, LogEventService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICartService, CartService>();
        }
    }
}
