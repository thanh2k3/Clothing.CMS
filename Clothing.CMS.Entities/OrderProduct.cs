using Clothing.CMS.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clothing.CMS.Entities
{
	[Table("OrderProduct")]
	public class OrderProduct : BaseEntity
	{
		// Order
		public int OrderId { get; set; }
		public virtual Order Order { get; set; }

		// Product
		public int ProductId { get; set; }
		public virtual Product Product { get; set; }
	}
}
