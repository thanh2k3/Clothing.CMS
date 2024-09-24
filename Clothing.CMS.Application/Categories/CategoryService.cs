using AutoMapper;
using Clothing.CMS.Application.Categories.Dto;
using Clothing.CMS.Entities;
using Clothing.CMS.EntityFrameworkCore.Pattern.Repositories;
using Clothing.Shared;
using Microsoft.EntityFrameworkCore;

namespace Clothing.CMS.Application.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repo;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ICollection<CategoryDto>> GetAll()
        {
            try
            {
                var result = await _repo.GetAll()
                  .Where(x => x.Status == StatusActivity.Active)
                  .OrderByDescending(x => x.Id)
                  .ToListAsync();

                var cateDto = _mapper.Map<ICollection<CategoryDto>>(result);

                return cateDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
