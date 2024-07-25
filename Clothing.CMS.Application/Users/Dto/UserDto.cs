using Clothing.CMS.Entities;

namespace Clothing.CMS.Application.Users.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime ModifiedTime { get; set; }
        public string? ModifiedBy { get; set; }
        
    }
}
