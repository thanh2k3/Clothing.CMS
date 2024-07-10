using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.Controllers
{
	public class HomeController : BaseController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
