using Clothing.CMS.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clothing.CMS.Entities
{
    [Table("Customer")]
    public class Customer : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
