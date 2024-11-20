using Clothing.CMS.Entities.Common;
using Clothing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Clothing.CMS.Application.Products.Dto
{
	public class CreateProductDto : BaseCruidEntity
	{
		[Display(Name = "Tên")]
		public string Name { get; set; }
		[Display(Name = "Mô tả")]
		public string? Description { get; set; }
		[Display(Name = "Giá bán")]
		public double Price { get; set; }
		[Display(Name = "Giá gốc")]
		public double OriginalPrice { get; set; }
		[Display(Name = "Số lượng")]
		public int Inventory { get; set; }
		[Display(Name = "Ảnh")]
		public string? ImageURL { get; set; }
		[Display(Name = "Loại")]
		public int CategoryId { get; set; }
		[Display(Name = "Trạng thái")]
		public StatusActivity Status { get; set; }
	}
}
