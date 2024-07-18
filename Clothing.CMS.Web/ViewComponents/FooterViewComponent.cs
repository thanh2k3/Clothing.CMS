using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.ViewComponents
{
	public class FooterViewComponent : ViewComponent
	{
		public FooterViewComponent() { }

		public IViewComponentResult Invoke(string filter)
		{
			return View();
		}
	}
}
