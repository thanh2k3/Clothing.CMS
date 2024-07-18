using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.ViewComponents
{
	public class ProductModalViewComponent : ViewComponent
	{
		public ProductModalViewComponent() { }

		public IViewComponentResult Invoke(string filter)
		{
			return View();
		}
	}
}
