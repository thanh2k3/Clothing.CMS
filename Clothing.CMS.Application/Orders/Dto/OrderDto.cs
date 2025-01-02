using Clothing.CMS.Entities.Authorization.Users;
using Clothing.CMS.Entities.Common;
using Clothing.Shared;

namespace Clothing.CMS.Application.Orders.Dto
{
	public class OrderDto : BaseCruidEntity
	{
		public string Code { get; set; }
		public string Address { get; set; }
		public double Total { get; set; }
		public OrderStatus OrderStatus { get; set; }
		public string StatusString { get; set; }
		public string UserEmail { get; set; }
		public User User { get; set; }
	}
}
