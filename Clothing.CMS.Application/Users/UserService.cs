using AutoMapper;
using Clothing.CMS.Application.Services;
using Clothing.CMS.Application.Users.Dto;
using Clothing.CMS.Entities.Authorization.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Clothing.CMS.Application.Users
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

		public async Task<IEnumerable<UserDto>> GetAll()
		{
			var users = await _userManager.Users.OrderByDescending(x => x.Id).ToListAsync();
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            return usersDto;
		}

		public async Task<bool> CreateAsync(CreateUserDto model)
		{
			try
            {
                var data = _mapper.Map<User>(model);

                var searchUN = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == data.UserName ||
                                                                        x.Email == data.Email);
                if (searchUN == null)
                {
					IdentityResult result = await _userManager.CreateAsync(data, model.Password);

                    if (result.Succeeded)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
		}
	}
}
