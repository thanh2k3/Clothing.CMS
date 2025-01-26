using AutoMapper;
using Clothing.CMS.Application.Orders;
using Clothing.CMS.Application.Orders.Dto;
using Clothing.CMS.Application.Users;
using Clothing.CMS.Web.Areas.Admin.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clothing.CMS.Web.Areas.Admin.Controllers
{
	[Authorize]
	public class OrderController : BaseController
	{
		private readonly IOrderService _orderService;
		private readonly IUserService _userService;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;
		public static List<SelectListItem> UserItems;

		public OrderController(IOrderService orderService, IUserService userService, IMapper mapper, ILoggerFactory loggerFactory)
		{
			_orderService = orderService;
			_userService = userService;
			_mapper = mapper;
			_logger = loggerFactory.CreateLogger<OrderController>();
		}

		public async Task<IActionResult> Index()
		{
			UserItems = await _userService.GetSelectListItemAsync();
			ViewBag.UserItems = UserItems;

			return View();
		}

		public async Task<JsonResult> GetData()
		{
			try
			{
				var orderDto = await _orderService.GetAll();
				var orderVM = _mapper.Map<ICollection<OrderViewModel>>(orderDto);

				_logger.LogInformation("Lấy ra tất cả đơn hàng");
				return Json(orderVM);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return Json(new { success = false, message = "Có lỗi xảy ra" });
			}
		}

		[HttpPost]
		public async Task<JsonResult> Create(CreateOrderViewModel model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					_logger.LogWarning("Thông tin không hợp lệ");
					return Json(new { success = false, message = "Thông tin không hợp lệ" });
				}

				var orderDto = _mapper.Map<CreateOrderDto>(model);
				var isSucceeded = await _orderService.CreateAsync(orderDto);
				if (!isSucceeded)
				{
					_logger.LogWarning((string?)TempData["Message"]);
					return Json(new { success = false, message = TempData["Message"] });
				}

				_logger.LogInformation((string?)TempData["Message"]);
				return Json(new { success = true, message = TempData["Message"] });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return Json(new { success = false, message = "Có lỗi xảy ra" });
			}
		}

		public async Task<ActionResult> EditModal(int id)
		{
			try
			{
				var orderDto = await _orderService.GetById(id);
				var orderVM = _mapper.Map<EditOrderViewModel>(orderDto);

				ViewBag.UserItems = UserItems;

				ViewBag.OrderProducts = orderVM.OrderProduct;

				return PartialView("_EditModal", orderVM);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return PartialView("_EditModal");
			}
		}

		[HttpPost]
		public async Task<JsonResult> Edit(EditOrderViewModel model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					_logger.LogWarning("Thông tin không hợp lệ");
					return Json(new { success = false, message = "Thông tin không hợp lệ" });
				}

				var orderDto = _mapper.Map<EditOrderDto>(model);
				var isSucceeded = await _orderService.UpdateAsync(orderDto);
				if (!isSucceeded)
				{
					_logger.LogWarning((string?)TempData["Message"]);
					return Json(new { success = false, message = TempData["Message"] });
				}

				_logger.LogInformation((string?)TempData["Message"]);
				return Json(new { success = true, message = TempData["Message"] });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return Json(new { success = false, message = "Có lỗi xảy ra" });
			}
		}
	}
}
