using AutoMapper;
using Clothing.CMS.Application.Categories.Dto;
using Clothing.CMS.Application.Common.Dto;
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

        public async Task<BaseResponse<ICollection<CategoryDto>>> GetAll()
        {
            try
            {
                var result = await _repo.GetAll()
                  .Where(x => x.Status == StatusActivity.Active || x.Status == StatusActivity.ActiveInternal)
                  .OrderByDescending(x => x.Id)
                  .ToListAsync();

                var cateDto = _mapper.Map<ICollection<CategoryDto>>(result);

				return BaseResponse<ICollection<CategoryDto>>.Ok(cateDto, "Lấy danh sách danh mục thành công");
			}
			catch (Exception ex)
            {
				throw new Exception(ex.Message);
			}
        }

        public async Task<BaseResponse<bool>> CreateAsync(CreateCategoryDto model)
		{
			try
			{
				var data = _mapper.Map<Category>(model);
                var searchCate = await _repo.FindAsync(x => x.Title == data.Title);
                if (searchCate == null)
                {
					FillAuthInfo(data);

					await _repo.AddAsync(data);

					return BaseResponse<bool>.Ok(true, "Thêm mới danh mục thành công");
				}

				return BaseResponse<bool>.Fail("Danh mục đã tồn tại");
			}
			catch (Exception ex)
			{
				return BaseResponse<bool>.Fail("Thêm mới danh mục thất bại");
			}
		}

		public async Task<BaseResponse<EditCategoryDto>> GetById(int id)
		{
            try
            {
                var cate = await _repo.FindAsync(x => x.Id == id);
                var cateDto = _mapper.Map<EditCategoryDto>(cate);

				return BaseResponse<EditCategoryDto>.Ok(cateDto);
			}
			catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
		}

		public async Task<BaseResponse<bool>> UpdateAsync(EditCategoryDto model)
		{
            try
            {
                var category = _mapper.Map<Category>(model);
                var existsCate = await _repo.FindAsync(x => x.Title == category.Title && x.Id != category.Id);
                if (existsCate == null)
                {
					FillAuthInfo(category);

					await _repo.UpdateAsync(category, category.Id);

                    return BaseResponse<bool>.Ok(true, "Chỉnh sửa danh mục thành công");
				}

				return BaseResponse<bool>.Fail("Danh mục đã tồn tại");
			}
            catch (Exception ex)
            {
				return BaseResponse<bool>.Fail("Chỉnh sửa danh mục thất bại");
			}
		}

        public async Task<BaseResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var cate = await _repo.FindAsync(x => x.Id == id);
                if (cate == null)
                {
					return BaseResponse<bool>.Fail("Không tìm thấy dữ liệu tương thích");
				}

				cate.Status = StatusActivity.InActive;
                await _repo.UpdateAsync(cate, id);

				return BaseResponse<bool>.Ok(true, "Xóa danh mục thành công");
			}
			catch (Exception ex)
            {
				return BaseResponse<bool>.Fail("Xóa danh mục thành công");
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
