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
}
