using Clothing.CMS.Application.Categories.Dto;

namespace Clothing.CMS.Application.Categories
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryDto>> GetAll();
        Task<bool> CreateAsync(CreateCategoryDto model);
    }
}
