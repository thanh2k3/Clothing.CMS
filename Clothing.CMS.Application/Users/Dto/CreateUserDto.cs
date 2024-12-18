using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Clothing.CMS.Entities.Common;

namespace Clothing.CMS.Application.Users.Dto
{
    public class CreateUserDto : BaseEntity
    {
		[DisplayName("Email")]
		public string Email { get; set; }

		[DisplayName("Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Nhập lại mật khẩu")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Tên")]
        public string FirstName { get; set; }

		[DisplayName("Họ")]
		public string LastName { get; set; }

		[DisplayName("Ảnh đại diện")]
        public string? AvatarURL { get; set; }
	}
}
