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
		public string Address { get; set; }
		public int Quantity { get; set; }
		public double Total { get; set; }
		public OrderStatus OrderStatus { get; set; }

		// Product
		public virtual ICollection<OrderProduct> OrderProducts { get; set; }

		// User
		public int? UserId { get; set; }
		public virtual User? User { get; set; }

		// Customer
		public int? CustomerId { get; set; }
		public virtual Customer? Customer { get; set; }

		// Sort delete
		public bool IsDeleted { get; set; }
	}
}
