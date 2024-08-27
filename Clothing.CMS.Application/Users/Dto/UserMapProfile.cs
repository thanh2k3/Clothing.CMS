using AutoMapper;
using Clothing.CMS.Entities.Authorization.Users;

namespace Clothing.CMS.Application.Users.Dto
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>()
                .ForMember(des => des.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(des => des.DateRegistered, opt => opt.MapFrom(src => DateTime.UtcNow.ToString()))
				.ForMember(des => des.Position, opt => opt.MapFrom(src => ""))
				.ForMember(des => des.NickName, opt => opt.MapFrom(src => ""));

            CreateMap<User, EditUserDto>();
        }
    }
}
