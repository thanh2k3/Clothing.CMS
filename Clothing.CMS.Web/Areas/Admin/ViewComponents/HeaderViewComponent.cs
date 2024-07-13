using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public HeaderViewComponent() { }

        public IViewComponentResult Invoke(string filter)
        {
            return View();
        }
    }
}
