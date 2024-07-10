using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
