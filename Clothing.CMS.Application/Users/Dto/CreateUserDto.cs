using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Clothing.CMS.Entities.Common;

namespace Clothing.CMS.Application.Users.Dto
{
    public class CreateUserDto : BaseCruidEntity
    {
        [DisplayName("Tên đăng nhập")]
        public string UserName { get; set; }
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }
        [DisplayName("Nhập lại mật khẩu")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [DisplayName("Tên")]
        public string FirstName { get; set; }
        [DisplayName("Ảnh đại diện")]
        public string Avatar { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
    }
}
