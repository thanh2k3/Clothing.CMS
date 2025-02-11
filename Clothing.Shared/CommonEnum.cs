using System.ComponentModel.DataAnnotations;

namespace Clothing.Shared
{
    public enum StatusActivity
    {
        [Display(Name = "Kích hoạt")]
        Active = 1,
        [Display(Name = "Kích hoạt nội bộ")]
        ActiveInternal = 2,
        [Display(Name = "Khóa")]
        InActive = 3
    }

    public enum OrderStatus
	{
		[Display(Name = "Chờ xác nhận")]
		Waiting = 1,
		[Display(Name = "Đã giao")]
		Delivered = 2,
		[Display(Name = "Đang giao")]
		OnDelivery = 3,
		[Display(Name = "Trả hàng")]
		Returns = 4,
		[Display(Name = "Đã hủy")]
		Canceled = 5
	}
}
