using AutoMapper;
using Clothing.CMS.Application.Orders;
using Clothing.CMS.Web.Areas.Admin.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.Controllers
{
	[Authorize]
	public class OrderController : BaseController
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;

		public OrderController(IOrderService orderService, IMapper mapper, ILoggerFactory loggerFactory)
		{
			_orderService = orderService;
			_mapper = mapper;
			_logger = loggerFactory.CreateLogger<OrderController>();
		}

		public IActionResult Index()
		{
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
	}
}
