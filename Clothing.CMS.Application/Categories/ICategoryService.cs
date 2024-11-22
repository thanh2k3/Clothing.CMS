using Clothing.CMS.Application.Categories.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clothing.CMS.Application.Categories
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryDto>> GetAll();
        Task<bool> CreateAsync(CreateCategoryDto model);
        Task<EditCategoryDto> GetById(int id);
		Task<bool> UpdateAsync(EditCategoryDto model);
        Task<bool> DeleteAsync(int id);
		Task<List<SelectListItem>> GetSelectListItemAsync();
	}
}
