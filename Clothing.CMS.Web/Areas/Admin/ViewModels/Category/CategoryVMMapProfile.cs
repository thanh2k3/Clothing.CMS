using AutoMapper;
using Clothing.CMS.Application.Categories.Dto;
using Clothing.Shared;

namespace Clothing.CMS.Web.Areas.Admin.ViewModels.Category
{
    public class CategoryVMMapProfile : Profile
    {
        public CategoryVMMapProfile()
        {
            CreateMap<CategoryDto, CategoryViewModel>()
                .ForMember(dest => dest.StatusString, opt => opt.MapFrom(src => src.Status.GetDescription()));
            CreateMap<CreateCategoryViewModel, CreateCategoryDto>();
            CreateMap<EditCategoryDto, EditCategoryViewModel>();
            CreateMap<EditCategoryViewModel, EditCategoryDto>();
        }
    }
}
