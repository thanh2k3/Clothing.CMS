using Clothing.Shared;
using Clothing.CMS.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Clothing.CMS.Application.Orders.Dto
{
	public class CreateOrderDto : BaseCruidEntity
	{
		[Display(Name = "Mã đơn hàng")]
		public string Code { get; set; }
		[Display(Name = "Ngày đặt hàng")]
		public DateTime Date { get; set; }
		[Display(Name = "Địa chỉ")]
		public string Address { get; set; }
		[Display(Name = "Tổng tiền")]
		public double Total { get; set; }
		[Display(Name = "Trạng thái đơn hàng")]
		public OrderStatus OrderStatus { get; set; }
		[Display(Name = "Sản phẩm")]
		public int ProductId { get; set; }
		[Display(Name = "Người đặt")]
		public int UserId { get; set; }
	}
}
