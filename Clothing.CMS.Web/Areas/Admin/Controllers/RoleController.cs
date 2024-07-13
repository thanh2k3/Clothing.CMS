using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class RoleController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
