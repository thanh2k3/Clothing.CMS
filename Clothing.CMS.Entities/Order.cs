using Clothing.CMS.Entities.Authorization.Users;
using Clothing.CMS.Entities.Common;
using Clothing.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clothing.CMS.Entities
{
	[Table("Order")]
	public class Order : BaseCruidEntity
	{
		public string Code { get; set; }
		public DateTime Date { get; set; }
		public string Address { get; set; }
		public int Quantity { get; set; }
		public double Total { get; set; }
		public OrderStatus OrderStatus { get; set; }

		// Sản phẩm
		public virtual ICollection<OrderProduct> OrderProducts { get; set; }

		// Người dùng
		public int UserId { get; set; }
		public virtual User User { get; set; }
	}
}
