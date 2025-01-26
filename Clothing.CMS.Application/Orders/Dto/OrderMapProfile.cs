﻿using AutoMapper;
using Clothing.CMS.Entities;

namespace Clothing.CMS.Application.Orders.Dto
{
	public class OrderMapProfile : Profile
	{
		public OrderMapProfile()
		{
			CreateMap<Order, OrderDto>();
			CreateMap<CreateOrderDto, Order>();
			CreateMap<Order, EditOrderDto>();
			CreateMap<EditOrderDto, Order>();
		}
	}
}
