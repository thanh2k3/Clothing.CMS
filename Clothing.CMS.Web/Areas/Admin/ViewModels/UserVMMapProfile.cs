using AutoMapper;
using Clothing.CMS.Application.Users.Dto;

namespace Clothing.CMS.Web.Areas.Admin.ViewModels
{
    public class UserVMMapProfile : Profile
    {
        public UserVMMapProfile()
        {
            CreateMap<CreateUserViewModel, CreateUserDto>();
        }
    }
}
