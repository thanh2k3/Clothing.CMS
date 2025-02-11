using Clothing.CMS.Entities.Common;
using Clothing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Clothing.CMS.Application.Orders.Dto
{
	public class EditOrderDto : BaseCruidEntity
	{
		[Display(Name = "Mã đơn hàng")]
		public string Code { get; set; }
		[Display(Name = "Địa chỉ")]
		public string Address { get; set; }
		public int Quantity { get; set; }
		[Display(Name = "Tổng tiền")]
		public double Total { get; set; }
		[Display(Name = "Trạng thái")]
		public OrderStatus OrderStatus { get; set; }
		[Display(Name = "Người đặt")]
		public int UserId { get; set; }

		// Danh sách sản phẩm liên quan đến đơn hàng
		public ICollection<EditOrderProductDto> OrderProduct { get; set; }
	}

	public class EditOrderProductDto
	{
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public double Price { get; set; }
		public bool IsActive { get; set; }
	}
}
