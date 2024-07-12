using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.Controllers
{
	[Authorize]
	public class HomeController : BaseController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
