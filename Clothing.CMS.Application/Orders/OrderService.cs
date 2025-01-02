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
		private readonly IRepository<OrderProduct> _orderProductRepo;
		private readonly IMapper _mapper;

		public OrderService(
			IRepository<Order> repo,
			IRepository<OrderProduct> orderProductRepo,
			IMapper mapper,
			IHttpContextAccessor httpContextAccessor,
			ITempDataDictionaryFactory tempDataDictionaryFactory) : base(httpContextAccessor, tempDataDictionaryFactory)
		{
			_repo = repo;
			_mapper = mapper;
			_orderProductRepo = orderProductRepo;
		}

		public async Task<ICollection<OrderDto>> GetAll()
		{
			try
			{
				var order = await _repo.GetAllIncluding(x => x.User)
					.OrderByDescending(x => x.Id)
					.ToListAsync();
				var orderDto = _mapper.Map<ICollection<OrderDto>>(order);

				return orderDto;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<bool> CreateAsync(CreateOrderDto model)
		{
			try
			{
				var order = _mapper.Map<Order>(model);
				var existsOrder = await _repo.FindAsync(x => x.Code == order.Code);
				if (existsOrder != null)
				{
					NotifyMsg("Sản phẩm đã tồn tại");
					return false;
				}

				if (model.OrderProduct == null && model.OrderProduct.Count <= 0)
				{
					NotifyMsg("Không có sản phẩm nào được chọn");
					return false;
				}

				FillAuthInfo(order);
				await _repo.AddAsync(order);

				var orderProducts = model.OrderProduct.Select(p => new OrderProduct
				{
					OrderId = order.Id,
					ProductId = p.ProductId,
				}).ToList();

				await _orderProductRepo.AddRangeAsync(orderProducts);

				NotifyMsg("Thêm mới sản phẩm thành công");
				return true;
			}
			catch (Exception ex)
			{
				NotifyMsg("Thêm mới sản phẩm thất bại");
				return false;
			}
		}
	}
}
