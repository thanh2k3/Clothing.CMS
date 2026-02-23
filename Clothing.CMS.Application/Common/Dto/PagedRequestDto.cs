namespace Clothing.CMS.Application.Common.Dto
{
    public class PagedRequestDto
    {
		/// <summary>
		/// Tìm kiếm theo keyword
		/// </summary>
		public string KeyWord { get; set; }
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 50;
		public int SkipCount { get { return (PageNumber - 1) * PageSize; } }
	}
}
