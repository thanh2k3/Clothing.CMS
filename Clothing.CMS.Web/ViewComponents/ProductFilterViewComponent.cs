using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.ViewComponents
{
	public class ProductFilterViewComponent : ViewComponent
	{
		public ProductFilterViewComponent() { }

		public IViewComponentResult Invoke(string filter)
		{
			return View();
		}
	}
}
