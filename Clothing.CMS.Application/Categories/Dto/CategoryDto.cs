using Clothing.CMS.Entities.Common;
using Clothing.Shared;

namespace Clothing.CMS.Application.Categories.Dto
{
    public class CategoryDto : BaseEntity
    {
        public string Title { get; set; }
        public StatusActivity Status { get; set; }
    }
}
