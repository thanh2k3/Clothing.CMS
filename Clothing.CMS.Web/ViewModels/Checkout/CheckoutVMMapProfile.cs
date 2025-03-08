using AutoMapper;
using Clothing.CMS.Application.Orders.Dto;

namespace Clothing.CMS.Web.ViewModels.Checkout
{
	public class CheckoutVMMapProfile : Profile
	{
		public CheckoutVMMapProfile()
		{
			CreateMap<CreateOrderDto, CheckoutViewModel>();
			CreateMap<CheckoutViewModel, CreateOrderDto>();
		}
	}
}
