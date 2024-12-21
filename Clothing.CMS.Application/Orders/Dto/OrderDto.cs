using Clothing.CMS.Entities.Authorization.Users;
using Clothing.CMS.Entities;
using Clothing.CMS.Entities.Common;
using Clothing.Shared;

namespace Clothing.CMS.Application.Orders.Dto
{
	public class OrderDto : BaseEntity
	{
		public string Code { get; set; }
		public DateTime Date { get; set; }
		public string Address { get; set; }
		public double Total { get; set; }
		public OrderStatus OrderStatus { get; set; }
		public int ProductId { get; set; }
		public Product Product { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
	}
}
