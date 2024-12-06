﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.Controllers
{
	[Authorize]
	public class OrderController : BaseController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
