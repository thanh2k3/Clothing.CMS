using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
