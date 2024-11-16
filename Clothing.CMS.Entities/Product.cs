using Clothing.CMS.Entities.Common;
using Clothing.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clothing.CMS.Entities
{
	[Table("Product")]
	public class Product : BaseCruidEntity
	{
		public string Name { get; set; }
		public string? Description { get; set; }
		public string Price { get; set; }
		public string OriginalPrice { get; set; }
		public int Inventory { get; set; }
		public string ImageURL { get; set; }
		public int CategoryId { get; set; }
		public virtual Category Category { get; set; }
		public StatusActivity Status { get; set; }
	}
}
