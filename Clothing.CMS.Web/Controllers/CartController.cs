using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Controllers
{
	public class CartController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
