using AutoMapper;
using Clothing.CMS.Application.Auths.Dto;

namespace Clothing.CMS.Web.Areas.Admin.ViewModels.Auth
{
	public class LoginVMMapProfile : Profile
	{
		public LoginVMMapProfile()
		{
			CreateMap<LoginViewModel, LoginDto>();
		}
	}
}
