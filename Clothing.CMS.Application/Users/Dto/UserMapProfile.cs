using AutoMapper;
using Clothing.CMS.Entities.Authorization.Users;

namespace Clothing.CMS.Application.Users.Dto
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<User, UserDto>();
			CreateMap<User, CreateUserDto>().ReverseMap();
		}
    }
}
