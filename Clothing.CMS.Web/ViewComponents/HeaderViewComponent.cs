using Clothing.CMS.Web.Common;
using Clothing.CMS.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public HeaderViewComponent() { }

        public IViewComponentResult Invoke(string filter)
        {
            var headers = new List<HeaderMenu>();

			headers.Add(ModuleHelper.AddModule(ModuleHelper.Module.Home));
			headers.Add(ModuleHelper.AddModule(ModuleHelper.Module.Product));
			headers.Add(ModuleHelper.AddModule(ModuleHelper.Module.Cart));
			headers.Add(ModuleHelper.AddModule(ModuleHelper.Module.Posts));
			headers.Add(ModuleHelper.AddModule(ModuleHelper.Module.About));
			headers.Add(ModuleHelper.AddModule(ModuleHelper.Module.Contact));

			return View(headers);
        }
    }
}
