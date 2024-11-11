using Clothing.CMS.Entities.Common;
using Clothing.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clothing.CMS.Entities
{
	[Table("Category")]
    public class Category : BaseCruidEntity
    {
        public string Title { get; set; }
        public StatusActivity Status { get; set; }
    }
}
