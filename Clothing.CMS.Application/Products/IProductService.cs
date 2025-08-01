using Clothing.CMS.Application.Common.Dto;
using Clothing.CMS.Application.Products.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clothing.CMS.Application.Products
{
	public interface IProductService
	{
		Task<BaseResponse<ICollection<ProductDto>>> GetAll();
		Task<BaseResponse<EditProductDto>> GetById(int id);
		Task<BaseResponse<ProductDto>> GetByIdIncluding(int id);
		Task<BaseResponse<bool>> CreateAsync(CreateProductDto model, IFormFile? image);
		Task<BaseResponse<bool>> UpdateAsync(EditProductDto model, IFormFile? image);
		Task<BaseResponse<bool>> DeleteAsync(int id);
		Task<List<SelectListItem>> GetSelectListItemAsync();
	}
}
