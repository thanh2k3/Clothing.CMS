using Clothing.CMS.Web.Areas.Admin.Models;

namespace Clothing.CMS.Web.Areas.Admin.Common
{
    /// <summary>
    /// This is where you customize the navigation sidebar
    /// </summary>
    public static class ModuleHelper
    {
        public enum Module
        {
            Home,

            Category,
            Product,

            User,
            Role,
        }

        public static SidebarMenu AddHeader(string name)
        {
            return new SidebarMenu
            {
                Type = SidebarMenuType.Header,
                Name = name,
            };
        }

        public static SidebarMenu AddTree(string name, string idName, string iconClassName = "fa fa-link")
        {
            return new SidebarMenu
            {
                Type = SidebarMenuType.Tree,
                IsActive = false,
                Name = name,
                IdName = idName,
                IconClassName = iconClassName,
                URLPath = "#",
            };
        }

        public static SidebarMenu AddModule(Module module, Tuple<int, int, int> counter = null)
        {
            if (counter == null)
                counter = Tuple.Create(0, 0, 0);

            switch (module)
            {
                case Module.Home:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Trang chủ",
                        IconClassName = "fa-solid fa-house",
                        URLPath = "/Admin",
                        LinkCounter = counter
                    };
                case Module.Category:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Danh mục",
                        IconClassName = "fas fa-bars",
                        URLPath = "/Admin/Category",
                        LinkCounter = counter
                    };
                case Module.Product:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Sản Phẩm",
                        IconClassName = "fa-solid fa-shirt",
                        URLPath = "/Admin/Product",
                        LinkCounter = counter,
                    };
                case Module.User:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Người dùng",
                        IconClassName = "fa fa-users",
                        URLPath = "/Admin/User",
                        LinkCounter = counter,
                    };
                case Module.Role:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Phân quyền",
                        IconClassName = "fa fa-user-tag",
                        URLPath = "/Admin/Role",
                        LinkCounter = counter,
                    };

                default:
                    break;
            }

            return null;
        }
    }
}
