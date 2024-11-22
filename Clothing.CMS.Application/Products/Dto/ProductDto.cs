using Clothing.CMS.Entities;
using Clothing.CMS.Entities.Common;
using Clothing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Clothing.CMS.Application.Products.Dto
{
	public class ProductDto : BaseEntity
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
		public string ImageURL { get; set; }
		public Category Category { get; set; }
		[Display(Name = "Trạng thái")]
		public string StatusString { get; set; }
		[Display(Name = "Loại")]
		public string CategoryTitle { get; set; }
	}
}
