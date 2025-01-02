using AutoMapper;
using Clothing.CMS.Application.Orders.Dto;
using Clothing.Shared;

namespace Clothing.CMS.Web.Areas.Admin.ViewModels.Order
{
	public class OrderVMMapProfile : Profile
	{
		public OrderVMMapProfile()
		{
			CreateMap<OrderDto, OrderViewModel>()
				.ForMember(dest => dest.StatusString, opt => opt.MapFrom(src => src.OrderStatus.GetDescription()));
			CreateMap<CreateOrderViewModel, CreateOrderDto>();
		}
	}
}
