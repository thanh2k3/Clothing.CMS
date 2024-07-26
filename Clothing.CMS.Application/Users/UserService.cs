using AutoMapper;
using Clothing.CMS.Application.Common.Dto;
using Clothing.CMS.Application.Services;
using Clothing.CMS.Application.Users.Dto;
using Clothing.CMS.Entities.Authorization.Users;
using Clothing.CMS.EntityFrameworkCore.Pattern;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Clothing.CMS.Application.Users
{
    public class UserService : BaseService, IUserService
    {
        private readonly CMSDbContext _context;
        private readonly IMapper _mapper;

        public UserService(CMSDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResponseDto<List<UserDto>>> GetAllPaging(UserPagedRequestDto input)
        {
            try
            {
                var query = _context.Set<User>()
                    .Where(x => string.IsNullOrEmpty(input.Keyword) || x.FirstName.Contains(input.Keyword));

                var list = await query.Skip(input.SkipCount).Take(input.PageSize).ToListAsync();

                var totalCount = await query.CountAsync();
                var data = _mapper.Map<List<UserDto>>(list);
                var result = new PagedResponseDto<List<UserDto>>(data, input.PageNumber, input.PageSize, totalCount, input.Keyword);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
