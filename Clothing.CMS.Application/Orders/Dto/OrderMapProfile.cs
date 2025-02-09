using AutoMapper;
using Clothing.CMS.Entities;
using Clothing.Shared;

namespace Clothing.CMS.Application.Orders.Dto
{
	public class OrderMapProfile : Profile
	{
		public OrderMapProfile()
		{
			CreateMap<Order, OrderDto>()
				.ForMember(dest => dest.StatusString, opt => opt.MapFrom(src => src.OrderStatus.GetDescription()));
			CreateMap<CreateOrderDto, Order>();
			CreateMap<Order, EditOrderDto>();
			CreateMap<EditOrderDto, Order>();
		}
	}
}
