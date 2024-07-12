using Microsoft.AspNetCore.Identity;

namespace Clothing.CMS.EntityFrameworkCore.Pattern
{
	// Add profile data for application users by adding properties to the CMSIdentityUser class
	public class CMSIdentityUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string? AvatarURL { get; set; }
		public string? DateRegistered { get; set; }
		public string? Position { get; set; }
		public string? NickName { get; set; }
	}
}
