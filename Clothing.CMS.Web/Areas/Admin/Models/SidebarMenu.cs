﻿namespace Clothing.CMS.Web.Areas.Admin.Models
{
	public class SidebarMenu
	{
		public SidebarMenuType Type { get; set; }
		public bool IsActive { get; set; } = false;
		public string Name { get; set; }
		public string IdName { get; set; }
		public string IconClassName { get; set; }
		public string URLPath { get; set; }
		public List<SidebarMenu> TreeChild { get; set; }
		public Tuple<int, int, int> LinkCounter { get; set; }
	}

	public enum SidebarMenuType
	{
		Header,
		Link,
		Tree
	}
}
