using Clothing.CMS.Entities;
using Clothing.CMS.Entities.Common;
using Clothing.Shared;

namespace Clothing.CMS.Application.Products.Dto
{
	public class ProductDto : BaseEntity
	{
		public string Name { get; set; }
		public string? Description { get; set; }
		public string Price { get; set; }
		public string OriginalPrice { get; set; }
		public int Inventory { get; set; }
		public string ImageURL { get; set; }
		public int CategoryId { get; set; }
		public virtual Category Category { get; set; }
	}
}
