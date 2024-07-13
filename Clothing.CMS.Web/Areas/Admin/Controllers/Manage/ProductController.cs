using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.Controllers.Manage
{
    [Authorize]
    public class ProductController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
