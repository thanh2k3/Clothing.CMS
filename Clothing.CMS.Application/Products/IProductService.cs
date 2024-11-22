using Clothing.CMS.Application.Products.Dto;
using Microsoft.AspNetCore.Http;

namespace Clothing.CMS.Application.Products
{
	public interface IProductService
	{
		Task<ICollection<ProductDto>> GetAll();
		Task<EditProductDto> GetById(int id);
		Task<ProductDto> GetByIdIncluding(int id);
		Task<bool> CreateAsync(CreateProductDto model, IFormFile? image);
		Task<bool> UpdateAsync(EditProductDto model, IFormFile? image);
		Task<bool> DeleteAsync(int id);
	}
}
