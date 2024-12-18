using Clothing.CMS.Entities.Common;
using System.ComponentModel;

namespace Clothing.CMS.Application.Users.Dto
{
    public class EditUserDto : BaseCruidEntity
    {
		[DisplayName("Email")]
		public string Email { get; set; }

        [DisplayName("Họ")]
        public string FirstName { get; set; }

        [DisplayName("Tên")]
        public string LastName { get; set; }

        [DisplayName("Ảnh đại diện")]
        public string? AvatarURL { get; set; }
    }
}
