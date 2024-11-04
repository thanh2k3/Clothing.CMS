using AutoMapper;
using Clothing.CMS.Application.Categories.Dto;
using Clothing.CMS.Application.Services;
using Clothing.CMS.Entities;
using Clothing.CMS.EntityFrameworkCore.Pattern.Repositories;
using Clothing.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace Clothing.CMS.Application.Categories
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IRepository<Category> _repo;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> repo,
            IMapper mapper,
			IHttpContextAccessor httpContextAccessor,
			ITempDataDictionaryFactory tempDataDictionaryFactory) : base(httpContextAccessor, tempDataDictionaryFactory)
		{
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ICollection<CategoryDto>> GetAll()
        {
            try
            {
                var result = await _repo.GetAll()
                  .Where(x => x.Status == StatusActivity.Active || x.Status == StatusActivity.ActiveInternal)
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

        public async Task<bool> CreateAsync(CreateCategoryDto model)
		{
			try
			{
				var data = _mapper.Map<Category>(model);
                var searchCate = await _repo.FindAsync(x => x.Title == data.Title);

                if (searchCate != null)
                {
                    NotifyMsg("Danh mục đã tồn tại");
                    return false;
                }

				FillAuthInfo(data);

				await _repo.AddAsync(data);

                NotifyMsg("Thêm mới danh mục thành công");
				return true;
			}
			catch (Exception ex)
			{
				NotifyMsg("Thêm mới danh mục thất bại");
				return false;
			}
		}
    }
}
