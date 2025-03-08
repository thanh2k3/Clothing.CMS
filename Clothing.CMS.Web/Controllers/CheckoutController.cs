using AutoMapper;
using Clothing.CMS.Application.Orders;
using Clothing.CMS.Application.Orders.Dto;
using Clothing.CMS.Web.ViewModels.Checkout;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IOrderService _orderService;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;

		public CheckoutController(IOrderService orderService, IMapper mapper, ILoggerFactory loggerFactory)
        {
            _orderService = orderService;
            _mapper = mapper;
			_logger = loggerFactory.CreateLogger<CheckoutController>();
		}

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Order(CheckoutViewModel model)
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
    }
}
