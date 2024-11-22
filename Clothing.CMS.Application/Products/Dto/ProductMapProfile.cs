using AutoMapper;
using Clothing.CMS.Entities;
using Clothing.Shared;

namespace Clothing.CMS.Application.Products.Dto
{
	public class ProductMapProfile : Profile
	{
		public ProductMapProfile()
		{
			CreateMap<Product, ProductDto>()
				.ForMember(dest => dest.StatusString, opt => opt.MapFrom(src => src.Status.GetDescription()));
			CreateMap<CreateProductDto, Product>();
			CreateMap<Product, EditProductDto>();
			CreateMap<EditProductDto, Product>();
		}
	}
}
