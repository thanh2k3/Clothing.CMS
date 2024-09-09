using AutoMapper;
using Clothing.CMS.Application.LogEvents.Dto;
using Clothing.CMS.Application.Services;
using Clothing.CMS.Entities;
using Clothing.CMS.EntityFrameworkCore.Pattern;
using Clothing.CMS.EntityFrameworkCore.Pattern.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace Clothing.CMS.Application.LogEvents
{
    public class LogEventService : BaseService, ILogEventService
    {
        private readonly IRepository<LogEvent> _repo;
        private readonly CMSDbContext _context;
        private readonly IMapper _mapper;

        public LogEventService(IRepository<LogEvent> repo,
            CMSDbContext context,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ITempDataDictionaryFactory tempDataDictionaryFactory) : base(httpContextAccessor, tempDataDictionaryFactory)
        {
            _repo = repo;
            _context = context;
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

        public async Task<bool> DeleteAllAsync()
        {
            try
            {
                string cmd = $"DELETE LogEvent";
                await _context.Database.ExecuteSqlRawAsync(cmd);
                NotifyMsg("Xóa nhật ký thành công");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
