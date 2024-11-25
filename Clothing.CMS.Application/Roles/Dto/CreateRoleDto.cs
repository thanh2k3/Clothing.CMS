using Clothing.CMS.Entities.Common;
using System.ComponentModel;

namespace Clothing.CMS.Application.Roles.Dto
{
	public class CreateRoleDto : BaseCruidEntity
	{
		[DisplayName("Tên quyền")]
		public string Name { get; set; }
		[DisplayName("Mô tả quyền")]
		public string Description { get; set; }
	}
}
