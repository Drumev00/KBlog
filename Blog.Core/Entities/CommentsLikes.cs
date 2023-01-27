using Blog.Core.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Entities
{
    public class CommentsLikes : BaseModel<string>
	{
		public CommentsLikes()
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
