using AutoMapper;
using Clothing.CMS.Application.Common.Dto;
using Clothing.CMS.Application.Products.Dto;
using Clothing.CMS.Application.Services;
using Clothing.CMS.Entities;
using Clothing.CMS.EntityFrameworkCore.Pattern.Repositories;
using Clothing.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace Clothing.CMS.Application.Products
{
	public class ProductService : BaseService, IProductService
	{
		private readonly IRepository<Product> _repo;
		private readonly IMapper _mapper;
		private readonly IHostingEnvironment _environment;

		public ProductService(
			IRepository<Product> repo,
			IMapper mapper,
			IHostingEnvironment environment,
			IHttpContextAccessor httpContextAccessor,
			ITempDataDictionaryFactory tempDataDictionaryFactory) : base(httpContextAccessor, tempDataDictionaryFactory)
		{
			_repo = repo;
			_mapper = mapper;
			_environment = environment;
		}

		public async Task<BaseResponse<ICollection<ProductDto>>> GetAll()
		{
			try
			{
				var result = await _repo.GetAllIncluding(x => x.Category)
					.Where(x => x.Status == StatusActivity.Active || x.Status == StatusActivity.ActiveInternal)
					.OrderByDescending(x => x.Id)
					.ToListAsync();

				var productDto = _mapper.Map<ICollection<ProductDto>>(result);

				return BaseResponse<ICollection<ProductDto>>.Ok(productDto, "Lấy danh sách sản phẩm thành công");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<BaseResponse<EditProductDto>> GetById(int id)
		{
			try
			{
				var product = await _repo.FindAsync(x => x.Id == id);
				var productDto = _mapper.Map<EditProductDto>(product);

				return BaseResponse<EditProductDto>.Ok(productDto);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<BaseResponse<ProductDto>> GetByIdIncluding(int id)
		{
			try
			{
				var product = await _repo.FindAsyncIncluding(x => x.Id == id, x => x.Category);
				var productDto = _mapper.Map<ProductDto>(product);

				return BaseResponse<ProductDto>.Ok(productDto);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<BaseResponse<bool>> CreateAsync(CreateProductDto model, IFormFile? image)
		{
			try
			{
				var product = _mapper.Map<Product>(model);
				var searchPrt = await _repo.FindAsync(x => x.Name.ToLower().Trim() == product.Name.Trim().ToLower());


				if (searchPrt != null)
					return BaseResponse<bool>.Fail("Sản phẩm đã tồn tại");

				if (image != null)
				{
					var imgName = "product-" + Guid.NewGuid().ToString() + "-" + image.FileName;
					var name = Path.Combine(_environment.WebRootPath + "/img/product", imgName);
					await image.CopyToAsync(new FileStream(name, FileMode.Create));
					product.ImageURL = "/img/product/" + imgName;
				}
				else
				{
					product.ImageURL = "/img/product/no-image.jpg";
				}

				FillAuthInfo(product);
				await _repo.AddAsync(product);

				return BaseResponse<bool>.Ok(true, "Thêm mới sản phẩm thành công");
			}
			catch (Exception ex)
			{
				return BaseResponse<bool>.Fail("Thêm mới sản phẩm thất bại");
			}
		}

		public async Task<BaseResponse<bool>> UpdateAsync(EditProductDto model, IFormFile? image)
		{
			try
			{
				var product = _mapper.Map<Product>(model);
				var existsPrt = await _repo.FindAsync(x => x.Name == product.Name && x.Id != product.Id);
				if (existsPrt == null)
				{
					if (image != null)
					{
						var imgName = "product-" + Guid.NewGuid().ToString() + "-" + image.FileName;
						var name = Path.Combine(_environment.WebRootPath + "/img/product", imgName);
						await image.CopyToAsync(new FileStream(name, FileMode.Create));
						product.ImageURL = "/img/product/" + imgName;
					}
					else
					{
						var existProduct = await _repo.FindAsync(x => x.Id == product.Id);
						if (existProduct != null && !string.IsNullOrEmpty(existProduct.ImageURL))
						{
							product.ImageURL = existProduct.ImageURL;
						}
					}

					FillAuthInfo(product);
					await _repo.UpdateAsync(product, product.Id);

					return BaseResponse<bool>.Ok(true, "Thêm mới sản phẩm thành công");
				}

				return BaseResponse<bool>.Fail("Sản phẩm đã tồn tại");
			}
			catch (Exception ex)
			{
				return BaseResponse<bool>.Fail("Chỉnh sửa sản phẩm thất bại");
			}
		}

		public async Task<BaseResponse<bool>> DeleteAsync(int id)
		{
			try
			{
				var product = await _repo.FindAsync(x => x.Id == id);
				if (product == null)
				{
					return BaseResponse<bool>.Fail("Không tìm thấy dữ liệu tương thích");
				}

				product.Status = StatusActivity.InActive;
				await _repo.UpdateAsync(product, id);

				return BaseResponse<bool>.Ok(true, "Xóa sản phẩm thành công");
			}
			catch (Exception ex)
			{
				return BaseResponse<bool>.Fail("Xóa sản phẩm thất bại");
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
						Text = x.Name,
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
