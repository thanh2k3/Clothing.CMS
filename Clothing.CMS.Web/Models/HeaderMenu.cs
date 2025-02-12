namespace Clothing.CMS.Web.Models
{
	public class HeaderMenu
	{
		public string Name { get; set; }
		public string URLPath { get; set; }
		public Tuple<int, int, int> LinkCounter { get; set; }
	}
}
