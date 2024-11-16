using AutoMapper;
using Clothing.CMS.Application.Products.Dto;

namespace Clothing.CMS.Web.Areas.Admin.ViewModels.Product
{
	public class ProductVMMapProfile : Profile
	{
		public ProductVMMapProfile()
		{
			CreateMap<ProductDto, ProductViewModel>();
		}
	}
}
