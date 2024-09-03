using Clothing.CMS.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clothing.CMS.Entities
{
	[Table("LogEvent")]
	public class LogEvent : BaseEntity
	{
		public string LogLevel { get; set; }
		public string Message { get; set; }
		public string Values { get; set; }
		public DateTime CreatedTime { get; set; }
	}
}
