using Microsoft.AspNetCore.Identity;

namespace Clothing.CMS.Entities.Authorization.Users
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? AvatarURL { get; set; }
        public string? DateRegistered { get; set; }
        public string? Position { get; set; }
        public string? NickName { get; set; }
    }
}
