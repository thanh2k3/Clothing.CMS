using AutoMapper;
using Clothing.CMS.Application.LogEvents.Dto;

namespace Clothing.CMS.Web.Areas.Admin.ViewModels.LogEvent
{
    public class LogEventVMMapProfile : Profile
    {
        public LogEventVMMapProfile()
        {
            CreateMap<LogEventDto, LogEventViewModel>();
        }
    }
}
