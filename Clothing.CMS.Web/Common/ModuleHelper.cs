using Clothing.CMS.Web.Models;

namespace Clothing.CMS.Web.Common
{
	public static class ModuleHelper
	{
		public enum Module
		{
			Home,
			Product,
			Cart,
			Posts,
			About,
			Contact
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
						LinkCounter = counter
					};
				case Module.Product:
					return new HeaderMenu
					{
						Name = "Sản phẩm",
						URLPath = "/Product",
						LinkCounter = counter
					};
				case Module.Cart:
					return new HeaderMenu
					{
						Name = "Giỏ hàng",
						URLPath = "/Cart",
						LinkCounter = counter
					};
				case Module.Posts:
					return new HeaderMenu
					{
						Name = "Bài viết",
						URLPath = "/Posts",
						LinkCounter = counter
					};
				case Module.About:
					return new HeaderMenu
					{
						Name = "Giới thiệu",
						URLPath = "/About",
						LinkCounter = counter
					};
				case Module.Contact:
					return new HeaderMenu
					{
						Name = "Liên hệ",
						URLPath = "/Contact",
						LinkCounter = counter
					};

				default:
					break;
			}

			return null;
		}
	}
}
