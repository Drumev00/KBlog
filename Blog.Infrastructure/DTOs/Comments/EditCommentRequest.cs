using System.ComponentModel.DataAnnotations;

namespace Blog.Infrastructure.DTOs.Comments
{
	public record EditCommentRequest
	{
		[Required]
		public string CommentId { get; set; } = null!;

		[Required]
		public string NewContent { get; set; } = null!;
	}
}
