using Blog.Core.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Blog.Core.Entities
{
    public class Role : IdentityRole, IAuditInfo
	{
		public Role()
			: this(null)
		{

		}
		public Role(string name)
			: base(name)
		{
			Id = Guid.NewGuid().ToString();
			CreatedOn = DateTime.UtcNow;
		}

		public DateTime CreatedOn { get; set; }
		public DateTime? ModifiedOn { get; set; }
	}
}
