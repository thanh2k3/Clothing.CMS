using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.Controllers
{
	public class RevenueController : BaseController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
