using Clothing.CMS.Entities.Common;
using System.ComponentModel;

namespace Clothing.CMS.Application.Roles.Dto
{
	public class EditRoleDto : BaseCruidEntity
	{
		[DisplayName("Tên quyền")]
		public string Name { get; set; }
		[DisplayName("Mô tả quyền")]
		public string Description { get; set; }
	}
}
