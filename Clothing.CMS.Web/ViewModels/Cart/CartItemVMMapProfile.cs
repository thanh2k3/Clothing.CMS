using AutoMapper;
using Clothing.CMS.Application.Carts.Dto;

namespace Clothing.CMS.Web.ViewModels.Cart
{
	public class CartItemVMMapProfile : Profile
	{
		public CartItemVMMapProfile()
		{
			CreateMap<CartItemViewModel, CartItemDto>();
			CreateMap<CartItemDto, CartItemViewModel>();
		}
	}
}
