using AutoMapper;
using Clothing.CMS.Application.Categories.Dto;

namespace Clothing.CMS.Web.Areas.Admin.ViewModels.Category
{
    public class CategoryVMMapProfile : Profile
    {
        public CategoryVMMapProfile()
        {
            CreateMap<CategoryDto, CategoryViewModel>();
        }
    }
}
