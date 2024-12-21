using AutoMapper;
using Clothing.CMS.Application.Orders.Dto;
using Clothing.CMS.Application.Services;
using Clothing.CMS.Entities;
using Clothing.CMS.EntityFrameworkCore.Pattern.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace Clothing.CMS.Application.Orders
{
	public class OrderService : BaseService, IOrderService
	{
		private readonly IRepository<Order> _repo;
		private readonly IMapper _mapper;

		public OrderService(
			IRepository<Order> repo,
			IMapper mapper,
			IHttpContextAccessor httpContextAccessor,
			ITempDataDictionaryFactory tempDataDictionaryFactory) : base(httpContextAccessor, tempDataDictionaryFactory)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<ICollection<OrderDto>> GetAll()
		{
			try
			{
				var order = await _repo.GetAll().OrderByDescending(x => x.Id).ToListAsync();
				var orderDto = _mapper.Map<ICollection<OrderDto>>(order);

				return orderDto;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
