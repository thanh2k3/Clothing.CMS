using Clothing.CMS.Web.Models;

namespace Clothing.CMS.Web.Common
{
	public static class ModuleHelper
	{
		public enum Module
		{
			Home,
			Product,
			ProductDetail,
			Cart,
			Posts,
			About,
			Contact,
			Checkout
		}

		public static HeaderMenu AddModule(Module module, Tuple<int, int, int> counter = null)
		{
			if (counter == null)
				counter = Tuple.Create(0, 0, 0);

			switch (module)
			{
				case Module.Home:
					return new HeaderMenu
					{
						Name = "Trang chủ",
						URLPath = "/",
						HeaderClass = "",
						WrapClass = "",
						IdName = "homeMenu",
						LinkCounter = counter
					};
				case Module.Product:
					return new HeaderMenu
					{
						Name = "Sản phẩm",
						URLPath = "/product",
						IdName = "productMenu",
						LinkCounter = counter
					};
				case Module.ProductDetail:
					return new HeaderMenu
					{
						URLPath = "/product/productdetail",
						IsActive = false,
						LinkCounter = counter
					};
				case Module.Cart:
					return new HeaderMenu
					{
						Name = "Giỏ hàng",
						URLPath = "/cart",
						IdName = "cartMenu",
						LinkCounter = counter
					};
				case Module.Posts:
					return new HeaderMenu
					{
						Name = "Bài viết",
						URLPath = "/posts",
						IdName = "postsMenu",
						LinkCounter = counter
					};
				case Module.About:
					return new HeaderMenu
					{
						Name = "Giới thiệu",
						URLPath = "/about",
						IdName = "aboutMenu",
						LinkCounter = counter
					};
				case Module.Contact:
					return new HeaderMenu
					{
						Name = "Liên hệ",
						URLPath = "/contact",
						IdName = "contactMenu",
						LinkCounter = counter
					};
				case Module.Checkout:
					return new HeaderMenu
					{
						URLPath = "/checkout",
						IsActive = false,
						LinkCounter = counter
					};

				default:
					break;
			}

			return null;
		}
	}
}
