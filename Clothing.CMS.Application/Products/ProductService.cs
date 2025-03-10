﻿using AutoMapper;
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

		public async Task<ICollection<ProductDto>> GetAll()
		{
			try
			{
				var result = await _repo.GetAllIncluding(x => x.Category)
					.Where(x => x.Status == StatusActivity.Active || x.Status == StatusActivity.ActiveInternal)
					.OrderByDescending(x => x.Id)
					.ToListAsync();

				var productDto = _mapper.Map<ICollection<ProductDto>>(result);

				return productDto;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<EditProductDto> GetById(int id)
		{
			try
			{
				var product = await _repo.FindAsync(x => x.Id == id);
				var productDto = _mapper.Map<EditProductDto>(product);

				return productDto;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<ProductDto> GetByIdIncluding(int id)
		{
			try
			{
				var product = await _repo.FindAsyncIncluding(x => x.Id == id, x => x.Category);
				var productDto = _mapper.Map<ProductDto>(product);

				return productDto;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<bool> CreateAsync(CreateProductDto model, IFormFile? image)
		{
			try
			{
				var product = _mapper.Map<Product>(model);
				var searchPrt = await _repo.FindAsync(x => x.Name == product.Name);
				if (searchPrt == null)
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
						product.ImageURL = "/img/product/no-image.jpg";
					}

					FillAuthInfo(product);
					await _repo.AddAsync(product);

					NotifyMsg("Thêm mới sản phẩm thành công");
					return true;
				}

				NotifyMsg("Sản phẩm đã tồn tại");
				return false;
			}
			catch (Exception ex)
			{
				NotifyMsg("Thêm mới sản phẩm thất bại");
				return false;
			}
		}

		public async Task<bool> UpdateAsync(EditProductDto model, IFormFile? image)
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

					NotifyMsg("Chỉnh sửa sản phẩm thành công");
					return true;
				}

				NotifyMsg("Sản phẩm đã tồn tại");
				return false;
			}
			catch (Exception ex)
			{
				NotifyMsg("Chỉnh sửa sản phẩm thất bại");
				return false;
			}
		}

		public async Task<bool> DeleteAsync(int id)
		{
			try
			{
				var product = await _repo.FindAsync(x => x.Id == id);
				if (product == null)
				{
					NotifyMsg("Không tìm thấy dữ liệu tương thích");
					return false;
				}

				product.Status = StatusActivity.InActive;
				await _repo.UpdateAsync(product, id);

				NotifyMsg("Xóa sản phẩm thành công");
				return true;
			}
			catch (Exception ex)
			{
				NotifyMsg("Xóa sản phẩm thất bại");
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
