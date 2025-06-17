namespace Clothing.CMS.Application.Common.Dto
{
    public class PagedResponseDto<T>
    {
		public T Items { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; } = 0;
		public string KeyWord { get; set; }
		public PagedResponseDto(T data, int pageNumber, int pageSize, int totalCount, string keyWord)
		{
			this.PageNumber = pageNumber;
			this.PageSize = pageSize;
			this.Items = data;
			this.TotalCount = totalCount;
			this.KeyWord = keyWord;
		}
	}
}
