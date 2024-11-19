using AutoMapper;
using Clothing.CMS.Entities;

namespace Clothing.CMS.Application.Products.Dto
{
	public class ProductMapProfile : Profile
	{
		public ProductMapProfile()
		{
			CreateMap<Product, ProductDto>();
			CreateMap<CreateProductDto, Product>();
		}
	}
}
