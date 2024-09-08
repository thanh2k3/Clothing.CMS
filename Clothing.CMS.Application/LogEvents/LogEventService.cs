using AutoMapper;
using Clothing.CMS.Application.LogEvents.Dto;
using Clothing.CMS.Entities;
using Clothing.CMS.EntityFrameworkCore.Pattern.Repositories;

namespace Clothing.CMS.Application.LogEvents
{
    public class LogEventService : ILogEventService
    {
        private readonly IRepository<LogEvent> _repo;
        private readonly IMapper _mapper;

        public LogEventService(IRepository<LogEvent> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ICollection<LogEventDto>> GetAll()
        {
            try
            {
                var logEvents = await _repo.GetAllAsync();
                var data = logEvents.OrderByDescending(x => x.Id);

                return _mapper.Map<ICollection<LogEventDto>>(data);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
