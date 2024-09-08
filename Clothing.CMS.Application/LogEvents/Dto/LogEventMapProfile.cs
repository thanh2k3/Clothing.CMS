using AutoMapper;
using Clothing.CMS.Entities;

namespace Clothing.CMS.Application.LogEvents.Dto
{
    public class LogEventMapProfile : Profile
    {
        public LogEventMapProfile()
        { 
            CreateMap<LogEvent, LogEventDto>();
        }
    }
}
