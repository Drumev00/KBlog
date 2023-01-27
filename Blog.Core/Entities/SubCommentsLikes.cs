using Blog.Core.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Entities
{
    public class SubCommentsLikes : BaseModel<string>
	{
		public SubCommentsLikes()
		{
			Id = Guid.NewGuid().ToString();
			CreatedOn = DateTime.UtcNow;
		}

		[Required]
		public string UserId { get; set; } = null!;

		public virtual User User { get; set; } = null!;

		[Required]
		public string SubCommentId { get; set; } = null!;

		public virtual SubComment SubComment { get; set; } = null!;
	}
}
