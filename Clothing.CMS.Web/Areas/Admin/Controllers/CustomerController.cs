using AutoMapper;
using Clothing.CMS.Application.Customers;
using Clothing.CMS.Web.Areas.Admin.ViewModels.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.Controllers
{
	[Authorize]
	public class CustomerController : BaseController
	{
		private readonly ICustomerService _customerService;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;

		public CustomerController(ICustomerService customerService, IMapper mapper, ILoggerFactory loggerFactory)
		{
			_customerService = customerService;
			_mapper = mapper;
			_logger = loggerFactory.CreateLogger<CustomerController>();
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<JsonResult> GetData()
		{
			try
			{
				var customerDto = await _customerService.GetAll();
				var customerVM = _mapper.Map<ICollection<CustomerViewModel>>(customerDto);

				_logger.LogInformation("Lấy ra tất cả khách hàng");
				return Json(customerVM);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return Json(new { success = false, message = "Có lỗi xảy ra" });
			}
		}
	}
}
