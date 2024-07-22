using Clothing.CMS.Entities;

namespace Clothing.CMS.Application.Users.Dto
{
    public class UserDto : BaseCruidEntity
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
    }
}
