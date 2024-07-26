using Clothing.CMS.Entities.Common;
using System.ComponentModel;

namespace Clothing.CMS.Application.Users.Dto
{
    public class EditUserDto : BaseCruidEntity
    {
        public string Email { get; set; }
        [DisplayName("Họ và tên")]
        public string FullName { get; set; }
        [DisplayName("Ngày sinh")]
        public DateTime Birthday { get; set; }
        [DisplayName("Ảnh đại diện")]
        public string Avatar { get; set; }
    }
}
