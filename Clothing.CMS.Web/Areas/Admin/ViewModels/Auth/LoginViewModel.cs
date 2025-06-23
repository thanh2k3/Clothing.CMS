using System.ComponentModel.DataAnnotations;

namespace Clothing.CMS.Web.Areas.Admin.ViewModels.Auth
{
	public class LoginViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Nhớ tôi?")]
		public bool RememberMe { get; set; }
	}
}
