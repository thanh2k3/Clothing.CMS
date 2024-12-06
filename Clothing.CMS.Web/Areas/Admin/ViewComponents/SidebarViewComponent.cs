using Clothing.CMS.Web.Areas.Admin.Common;
using Clothing.CMS.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        public SidebarViewComponent() { }

        public IViewComponentResult Invoke(string filter)
        {
            // you can do the access rights checking here by using session, user, and/or filter parameter
            var sidebars = new List<SidebarMenu>();

            sidebars.Add(ModuleHelper.AddModule(ModuleHelper.Module.Home));

			sidebars.Add(ModuleHelper.AddTree("Quản lý sản phẩm", "product-item", "fa-solid fa-box"));
			sidebars.Last().TreeChild = new List<SidebarMenu>()
			{
				ModuleHelper.AddModule(ModuleHelper.Module.Product),
			};

			sidebars.Add(ModuleHelper.AddTree("Quản lý đơn hàng", "order-item", "fa-solid fa-cart-shopping"));
			sidebars.Last().TreeChild = new List<SidebarMenu>()
			{
				ModuleHelper.AddModule(ModuleHelper.Module.Order),
			};

			sidebars.Add(ModuleHelper.AddTree("Quản lý danh mục", "category-item", "fa-solid fa-list"));
            sidebars.Last().TreeChild = new List<SidebarMenu>()
            {
                ModuleHelper.AddModule(ModuleHelper.Module.Category),
            };

			sidebars.Add(ModuleHelper.AddTree("Quản lý khách hàng", "customer-item", "fa-solid fa-user"));
			sidebars.Last().TreeChild = new List<SidebarMenu>()
			{
				ModuleHelper.AddModule(ModuleHelper.Module.Customer),
			};

			sidebars.Add(ModuleHelper.AddTree("Quản lý thu chi", "profit-item", "fa-solid fa-hand-holding-dollar"));
            sidebars.Last().TreeChild = new List<SidebarMenu>()
            {
                ModuleHelper.AddModule(ModuleHelper.Module.Revenue),
                ModuleHelper.AddModule(ModuleHelper.Module.Expense),
            };

			if (User.IsInRole("SuperAdmin"))
            {
                sidebars.Add(ModuleHelper.AddTree("Quản trị", "role-item", "fa-solid fa-user-gear"));
                sidebars.Last().TreeChild = new List<SidebarMenu>()
                {
                    ModuleHelper.AddModule(ModuleHelper.Module.User),
                    ModuleHelper.AddModule(ModuleHelper.Module.Role),
                };

				sidebars.Add(ModuleHelper.AddTree("Quản lý hệ thống", "logEvent-item", "fa-solid fa-gear"));
				sidebars.Last().TreeChild = new List<SidebarMenu>()
				{
					ModuleHelper.AddModule(ModuleHelper.Module.LogEvent),
				};
			}

            return View(sidebars);
        }
    }
}
