using AutoMapper;
using Clothing.CMS.Application.LogEvents;
using Clothing.CMS.Web.Areas.Admin.ViewModels.LogEvent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.Controllers
{
	[Authorize]
	public class LogEventController : BaseController
	{
		private readonly ILogEventService _logEventService;
		private readonly IMapper _mapper;
		private readonly ILogger<LogEventController> _logger;

		public LogEventController(ILogEventService logEventService, IMapper mapper, ILogger<LogEventController> logger)
		{
			_logEventService = logEventService;
			_mapper = mapper;
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

        public async Task<JsonResult> GetData()
        {
			try
			{
				var logEventDto = await _logEventService.GetAll();
				var logEventVM = _mapper.Map<ICollection<LogEventViewModel>>(logEventDto);

				return Json(logEventVM);
			}
			catch (Exception ex)
			{
                _logger.LogError(ex.Message);

                return Json(null);
            }
        }

		[HttpPost]
		public async Task<IActionResult> DeleteLogEvent()
		{
			try
			{
				var isSucceeded = await _logEventService.DeleteAllAsync();

                if (isSucceeded)
                {
                    return Json(new { success = true, message = TempData["Message"] });
                }

				throw new Exception("Xóa nhật ký thất bại");
            }
            catch (Exception ex)
			{
                return Json(new { success = false, message = ex.Message });
            }
		}
    }
}
