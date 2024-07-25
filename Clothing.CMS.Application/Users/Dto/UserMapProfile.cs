using AutoMapper;
using Clothing.CMS.EntityFrameworkCore.Pattern;

namespace Clothing.CMS.Application.Users.Dto
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<CMSIdentityUser, UserDto>();

            //CreateMap<CreateUserDto, CMSIdentityUser>();
            //CreateMap<EditUserDto, CMSIdentityUser>();
        }
    }
}
