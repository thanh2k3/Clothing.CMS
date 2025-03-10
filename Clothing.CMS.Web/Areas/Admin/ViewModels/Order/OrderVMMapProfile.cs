﻿using AutoMapper;
using Clothing.CMS.Application.Orders.Dto;

namespace Clothing.CMS.Web.Areas.Admin.ViewModels.Order
{
	public class OrderVMMapProfile : Profile
	{
		public OrderVMMapProfile()
		{
			CreateMap<OrderDto, OrderViewModel>();
			CreateMap<CreateOrderViewModel, CreateOrderDto>();
			CreateMap<EditOrderDto, EditOrderViewModel>();
			CreateMap<EditOrderViewModel, EditOrderDto>();
		}
	}
}
