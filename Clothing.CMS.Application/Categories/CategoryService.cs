using AutoMapper;
using Clothing.CMS.Application.Categories.Dto;
using Clothing.CMS.Application.Services;
using Clothing.CMS.Entities;
using Clothing.CMS.EntityFrameworkCore.Pattern.Repositories;
using Clothing.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace Clothing.CMS.Application.Categories
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IRepository<Category> _repo;
        private readonly IMapper _mapper;

        public CategoryService(
            IRepository<Category> repo,
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
				throw new Exception(ex.Message);
			}
        }

        public async Task<bool> CreateAsync(CreateCategoryDto model)
		{
			try
			{
				var data = _mapper.Map<Category>(model);
                var searchCate = await _repo.FindAsync(x => x.Title == data.Title);
                if (searchCate == null)
                {
					FillAuthInfo(data);

					await _repo.AddAsync(data);

					NotifyMsg("Thêm mới danh mục thành công");
					return true;
				}

				NotifyMsg("Danh mục đã tồn tại");
				return false;
			}
			catch (Exception ex)
			{
				NotifyMsg("Thêm mới danh mục thất bại");
				return false;
			}
		}

		public async Task<EditCategoryDto> GetById(int id)
		{
            try
            {
                var cate = await _repo.FindAsync(x => x.Id == id);
                var cateDto = _mapper.Map<EditCategoryDto>(cate);

                return cateDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
		}

		public async Task<bool> UpdateAsync(EditCategoryDto model)
		{
            try
            {
                var category = _mapper.Map<Category>(model);
                var existsCate = await _repo.FindAsync(x => x.Title == category.Title && x.Id != category.Id);
                if (existsCate == null)
                {
					FillAuthInfo(category);

					await _repo.UpdateAsync(category, category.Id);

					NotifyMsg("Chỉnh sửa danh mục thành công");
					return true;
				}

				NotifyMsg("Danh mục đã tồn tại");
				return false;
			}
            catch (Exception ex)
            {
				NotifyMsg("Chỉnh sửa danh mục thất bại");
				return false;
            }
		}

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var cate = await _repo.FindAsync(x => x.Id == id);
                if (cate == null)
                {
                    NotifyMsg("Không tìm thấy dữ liệu tương thích");
                    return false;
                }

                cate.Status = StatusActivity.InActive;
                await _repo.UpdateAsync(cate, id);

                NotifyMsg("Xóa danh mục thành công");
                return true;
            }
            catch (Exception ex)
            {
                NotifyMsg("Xóa danh mục thất bại");
                return false;
            }
        }

        public async Task<List<SelectListItem>> GetSelectListItemAsync()
        {
            try
            {
                var result = _repo.GetAll()
					.Where(x => x.Status == StatusActivity.Active || x.Status == StatusActivity.ActiveInternal)
                    .Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Title,
                    })
                    .ToList();

                return result;
			}
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
		}
	}
}
