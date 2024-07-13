using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.ViewComponents
{
    public class MenuMessageViewComponent : ViewComponent
    {
        public MenuMessageViewComponent() { }

        public IViewComponentResult Invoke(string filter)
        {
            return View();
        }
    }
}
