using AutoMapper;
using Clothing.CMS.Application.Users.Dto;

namespace Clothing.CMS.Web.Areas.Admin.ViewModels.User
{
	public class UserVMMapProfile : Profile
	{
		public UserVMMapProfile()
		{
			CreateMap<UserDto, UserViewModel>();
			CreateMap<CreateUserViewModel, CreateUserDto>();
			CreateMap<EditUserDto, EditUserViewModel>();
			CreateMap<EditUserViewModel, EditUserDto>();
		}
	}
}
