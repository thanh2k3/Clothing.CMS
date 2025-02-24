namespace Clothing.CMS.Application.Carts.Dto
{
	public class CartItemDto
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public string ImageURL { get; set; }
		public int Price { get; set; }
		public int Quantity { get; set; }
		public string Size { get; set; }
		public string Color { get; set; }
	}
}
