namespace Clothing.CMS.Web.Models
{
	public class HeaderMenu
	{
		public string Name { get; set; }
		public string URLPath { get; set; }
		public string HeaderClass { get; set; } = "header-v4";
		public string WrapClass { get; set; } = "how-shadow1";
		public string IdName { get; set; }
		public bool IsActive { get; set; } = true;
		public Tuple<int, int, int> LinkCounter { get; set; }
	}
}
