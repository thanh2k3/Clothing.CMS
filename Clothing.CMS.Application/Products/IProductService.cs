using Clothing.CMS.Application.Products.Dto;

namespace Clothing.CMS.Application.Products
{
	public interface IProductService
	{
		Task<ICollection<ProductDto>> GetAll();
	}
}
