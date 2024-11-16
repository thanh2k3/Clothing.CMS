using AutoMapper;
using Clothing.CMS.Application.Products.Dto;
using Clothing.CMS.Application.Services;
using Clothing.CMS.Entities;
using Clothing.CMS.EntityFrameworkCore.Pattern.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace Clothing.CMS.Application.Products
{
	public class ProductService : BaseService, IProductService
	{
		private readonly IRepository<Product> _repo;
		private readonly IMapper _mapper;

		public ProductService(
			IRepository<Product> repo,
			IMapper mapper,
			IHttpContextAccessor httpContextAccessor,
			ITempDataDictionaryFactory tempDataDictionaryFactory) : base(httpContextAccessor, tempDataDictionaryFactory)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<ICollection<ProductDto>> GetAll()
		{
			try
			{
				var result = await _repo.GetAll()
					.OrderByDescending(x => x.Id)
					.ToListAsync();

				var productDto = _mapper.Map<ICollection<ProductDto>>(result);

				return productDto;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
