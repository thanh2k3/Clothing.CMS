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
					.Where(x => !x.IsDeleted)
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

		public async Task<EditOrderDto> GetById(int id)
		{
			try
			{
				var order = await _repo.FindAsync(x => x.Id == id);
				if (order == null)
				{
					throw new KeyNotFoundException($"Không tìm thấy người dùng với ID: \"{id}\"");
				}

				var orderDto = _mapper.Map<EditOrderDto>(order);

				var orderProduct = await _orderProductRepo.FindAllAsync(x => x.OrderId == id);

				orderDto.OrderProduct = orderProduct.Select(op => new EditOrderProductDto
				{
					OrderId = op.OrderId,
					ProductId = op.ProductId,
					Quantity = op.Quantity,
					Price = op.Price,
					IsActive = op.IsActitve
				}).ToList();

				return orderDto;
			}
			catch (KeyNotFoundException ex)
			{
				throw new Exception(ex.Message);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<OrderDto> GetByIdIncluding(int id)
		{
			try
			{
				var order = await _repo.FindAsyncIncluding(x => x.Id == id, x => x.User);
				if (order == null)
				{
					throw new KeyNotFoundException($"Không tìm thấy người dùng với ID: \"{id}\"");
				}

				var orderDto = _mapper.Map<OrderDto>(order);

				var orderProduct = await _orderProductRepo.FindAllAsync(x => x.OrderId == id);
				orderDto.OrderProduct = orderProduct.Select(op => new OrderProductDto
				{
					OrderId = op.OrderId,
					ProductId = op.ProductId,
					Quantity = op.Quantity,
					Price = op.Price,
					IsActive = op.IsActitve
				}).ToList();

				return orderDto;
			}
			catch (KeyNotFoundException ex)
			{
				throw new Exception(ex.Message);
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
				var existsOrder = await _repo.FindAsync(x => x.Code == order.Code && x.IsDeleted == false);
				if (existsOrder != null)
				{
					NotifyMsg("Đơn hàng đã tồn tại");
					return false;
				}

				FillAuthInfo(order);
				await _repo.AddAsync(order);

				var orderProducts = model.OrderProduct.Select(p => new OrderProduct
				{
					OrderId = order.Id,
					ProductId = p.ProductId,
					Quantity = p.Quantity,
					Price = p.Price,
					IsActitve = p.IsActive
				}).ToList();

				await _orderProductRepo.AddRangeAsync(orderProducts);

				NotifyMsg("Thêm mới đơn hàng thành công");
				return true;
			}
			catch (Exception ex)
			{
				NotifyMsg("Thêm mới đơn hàng thất bại");
				return false;
			}
		}

		public async Task<bool> UpdateAsync(EditOrderDto model)
		{
			try
			{
				var order = _mapper.Map<Order>(model);
				var existsOrder = await _repo.FindAsync(x => x.Code == order.Code 
														&& x.Id != order.Id 
														&& x.IsDeleted == false);
				if (existsOrder != null)
				{
					NotifyMsg("Đơn hàng đã tồn tại");
					return false;
				}

				FillAuthInfo(order);
				await _repo.UpdateAsync(order, order.Id);

				// Lấy danh sách sản phẩm cũ của đơn hàng
				var existsOProduct = await _orderProductRepo.FindAllAsync(x => x.OrderId == order.Id);

				// Xóa các sản phẩm không còn trong danh sách mới
				var productsToRemove = existsOProduct
					.Where(op => !model.OrderProduct.Any(p => p.ProductId == op.ProductId))
					.ToList();

				if (productsToRemove.Any())
				{
					await _orderProductRepo.DeleteRangeAsync(productsToRemove);
				}

				// Thêm hoặc cập nhật các sản phẩm mới
				var updateOrderProducts = model.OrderProduct.Select(p => new OrderProduct
				{
					OrderId = order.Id,
					ProductId = p.ProductId,
					Quantity = p.Quantity,
					Price = p.Price,
					IsActitve = p.IsActive
				}).ToList();

				foreach (var item in updateOrderProducts)
				{
					var product = existsOProduct.FirstOrDefault(op => op.ProductId == item.ProductId);
					if (product != null)
					{
						// Cập nhật sản phẩm đã tồn tại
						product.Quantity = item.Quantity;
						product.Price = item.Price;
						product.IsActitve = item.IsActitve;
						await _orderProductRepo.UpdateAsync(product, product.Id);
					}
					else
					{
						// Thêm sản phẩm mới
						await _orderProductRepo.AddAsync(item);
					}
				}

				NotifyMsg("Chỉnh sửa đơn hàng thành công");
				return true;
			}
			catch (Exception ex)
			{
				NotifyMsg("Chỉnh sửa đơn hàng thất bại");
				return false;
			}
		}

		public async Task<bool> DeleteAsync(int id)
		{
			try
			{
				var order = await _repo.FindAsync(x => x.Id == id);
				if (order == null)
				{
					NotifyMsg("Không tìm thấy đơn hàng");
					return false;
				}

				order.IsDeleted = true;
				FillAuthInfo(order);
				await _repo.UpdateAsync(order, order.Id);

				var orderProduct = await _orderProductRepo.FindAllAsync(x => x.OrderId == order.Id);
				foreach (var item in orderProduct)
				{
					item.IsDeleted = true;
					await _orderProductRepo.UpdateAsync(item, item.Id);
				}

				NotifyMsg("Xóa đơn hàng thành công");
				return true;
			}
			catch (Exception ex)
			{
				NotifyMsg("Xóa đơn hàng thất bại");
				return false;
			}
		}
	}
}
