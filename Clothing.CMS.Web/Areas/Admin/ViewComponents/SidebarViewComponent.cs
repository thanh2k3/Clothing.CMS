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

            sidebars.Add(ModuleHelper.AddTree("Quản lý", "manage-item", "fa-solid fa-list"));
            sidebars.Last().TreeChild = new List<SidebarMenu>()
            {
                ModuleHelper.AddModule(ModuleHelper.Module.Category),
                ModuleHelper.AddModule(ModuleHelper.Module.Product),
            };

            if (User.IsInRole("SuperAdmin"))
            {
                sidebars.Add(ModuleHelper.AddTree("Quản trị", "role-item", "fa-solid fa-user-gear"));
                sidebars.Last().TreeChild = new List<SidebarMenu>()
                {
                    ModuleHelper.AddModule(ModuleHelper.Module.User),
                    ModuleHelper.AddModule(ModuleHelper.Module.Role),
                    ModuleHelper.AddModule(ModuleHelper.Module.LogEvent),
                };
            }

            return View(sidebars);
        }
    }
}
