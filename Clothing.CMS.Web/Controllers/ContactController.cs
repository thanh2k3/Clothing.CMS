using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
