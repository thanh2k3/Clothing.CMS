using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.ViewComponents
{
    public class MenuUserViewComponent : ViewComponent
    {
        public MenuUserViewComponent() { }

        public IViewComponentResult Invoke(string filter)
        {
            return View();
        }
    }
}
