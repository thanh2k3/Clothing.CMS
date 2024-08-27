using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Clothing.CMS.Entities.Common;

namespace Clothing.CMS.Application.Users.Dto
{
    public class UserDto : BaseEntity
    {
        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Tên")]
        public string FirstName { get; set; }

        [DisplayName("Họ")]
        public string LastName { get; set; }

        [DisplayName("Ảnh đại diện")]
        public string AvatarURL { get; set; }
    }
}
