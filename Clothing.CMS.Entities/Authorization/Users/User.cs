using Microsoft.AspNetCore.Identity;

namespace Clothing.CMS.Entities.Authorization.Users
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? AvatarURL { get; set; }
        public string? Position { get; set; }
        public string? NickName { get; set; }

		// Bắt buộc
		public DateTime CreatedTime { get; set; }
		public string? CreatedBy { get; set; }
		public DateTime ModifiedTime { get; set; }
		public string? ModifiedBy { get; set; }

		// Sort delete
		public bool IsDeleted { get; set; }
	}
}
