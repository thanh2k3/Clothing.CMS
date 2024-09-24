using AutoMapper;
using Clothing.CMS.Entities;

namespace Clothing.CMS.Application.Categories.Dto
{
    public class CategoryMapProfile : Profile
    {
        public CategoryMapProfile()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
