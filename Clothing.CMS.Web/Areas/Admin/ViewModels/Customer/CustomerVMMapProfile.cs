using AutoMapper;
using Clothing.CMS.Application.Customers.Dto;

namespace Clothing.CMS.Web.Areas.Admin.ViewModels.Customer
{
	public class CustomerVMMapProfile : Profile
	{
		public CustomerVMMapProfile()
		{
			CreateMap<CustomerDto, CustomerViewModel>();
		}
	}
}
