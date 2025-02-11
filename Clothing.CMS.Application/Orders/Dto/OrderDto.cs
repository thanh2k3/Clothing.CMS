using Clothing.CMS.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Clothing.CMS.Application.Orders.Dto
{
	public class OrderDto : BaseCruidEntity
	{
		[Display(Name = "Mã đơn hàng")]
		public string Code { get; set; }
		[Display(Name = "Địa chỉ")]
		public string Address { get; set; }
		[Display(Name = "Tổng tiền")]
		public double Total { get; set; }
		[Display(Name = "Trạng thái")]
		public string StatusString { get; set; }
		[Display(Name = "Email")]
		public string UserEmail { get; set; }

		// Danh sách sản phẩm liên quan đến đơn hàng
		public ICollection<OrderProductDto> OrderProduct { get; set; }
	}

	public class OrderProductDto
	{
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public double Price { get; set; }
		public bool IsActive { get; set; }
	}
}
