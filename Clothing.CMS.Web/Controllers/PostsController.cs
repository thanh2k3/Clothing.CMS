using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
