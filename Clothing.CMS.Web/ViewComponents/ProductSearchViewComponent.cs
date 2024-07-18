using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.ViewComponents
{
	public class ProductSearchViewComponent : ViewComponent
	{
		public ProductSearchViewComponent() { }

		public IViewComponentResult Invoke(string filter)
		{
			return View();
		}
	}
}
