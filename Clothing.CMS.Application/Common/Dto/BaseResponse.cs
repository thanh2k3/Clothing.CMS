namespace Clothing.CMS.Application.Common.Dto
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; } = true;
		public string Message { get; set; } = "";
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

		public static BaseResponse<T> Ok(T data, string message = "Thành công")
		{
			return new BaseResponse<T>
			{
				Success = true,
				Message = message,
				Data = data
			};
		}

		public static BaseResponse<T> Fail(string message = "Thất bại", List<string>? errors = null)
		{
			return new BaseResponse<T>
			{
				Success = false,
				Message = message,
				Errors = errors
			};
		}
	}
}
