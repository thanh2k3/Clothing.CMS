using Clothing.CMS.Application.LogEvents.Dto;

namespace Clothing.CMS.Application.LogEvents
{
    public interface ILogEventService
    {
        Task<ICollection<LogEventDto>> GetAll();
        Task<bool> DeleteAllAsync();
    }
}
