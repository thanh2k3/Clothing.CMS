using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.ViewComponents
{
    public class MenuNotificationViewComponent : ViewComponent
    {
        public MenuNotificationViewComponent() { }

        public IViewComponentResult Invoke(string filter)
        {
            return View();
        }
    }
}
