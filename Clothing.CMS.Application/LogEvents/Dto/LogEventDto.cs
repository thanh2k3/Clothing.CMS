using Clothing.CMS.Entities.Common;

namespace Clothing.CMS.Application.LogEvents.Dto
{
    public class LogEventDto : BaseEntity
    {
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public string Values { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
