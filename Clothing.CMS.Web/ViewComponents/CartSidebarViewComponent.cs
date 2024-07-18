using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.ViewComponents
{
	public class CartSidebarViewComponent : ViewComponent
	{
		public CartSidebarViewComponent() { }
		
		public IViewComponentResult Invoke(string filter)
		{
			return View();
		}
	}
}
