using Clothing.CMS.Entities.Common;
using Clothing.Shared;
using System.ComponentModel;

namespace Clothing.CMS.Application.Categories.Dto
{
	public class EditCategoryDto : BaseCruidEntity
	{
		[DisplayName("Tiêu đề")]
		public string Title { get; set; }
		[DisplayName("Trạng thái")]
		public StatusActivity Status { get; set; }
	}
}
