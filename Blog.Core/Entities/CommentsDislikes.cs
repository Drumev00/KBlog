using System.ComponentModel.DataAnnotations;
using Blog.Core.Abstractions;

namespace Blog.Core.Entities
{
	public class CommentsDislikes : BaseModel<string>
	{
		public CommentsDislikes()
		{
			Id = Guid.NewGuid().ToString();
			CreatedOn = DateTime.UtcNow;
		}

		[Required]
		public string CommentId { get; set; } = null!;

		public virtual Comment Comment { get; set; } = null!;

		[Required]
		public string UserId { get; set; } = null!;

		public virtual User User { get; set; } = null!;
	}
}
