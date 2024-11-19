using Clothing.CMS.Application.Products.Dto;
using Microsoft.AspNetCore.Http;

namespace Clothing.CMS.Application.Products
{
	public interface IProductService
	{
		Task<ICollection<ProductDto>> GetAll();
		Task<bool> CreateAsync(CreateProductDto model, IFormFile? image);
	}
}
