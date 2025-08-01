using Clothing.CMS.Application.Categories.Dto;
using Clothing.CMS.Application.Common.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clothing.CMS.Application.Categories
{
    public interface ICategoryService
    {
        Task<BaseResponse<ICollection<CategoryDto>>> GetAll();
        Task<BaseResponse<bool>> CreateAsync(CreateCategoryDto model);
        Task<BaseResponse<EditCategoryDto>> GetById(int id);
		Task<BaseResponse<bool>> UpdateAsync(EditCategoryDto model);
        Task<BaseResponse<bool>> DeleteAsync(int id);
		Task<List<SelectListItem>> GetSelectListItemAsync();
	}
}
