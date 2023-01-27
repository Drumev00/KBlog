using System.ComponentModel.DataAnnotations;

namespace Blog.Infrastructure.DTOs.Comments
{
	public record CommentRequest
	{
		[Required]
		public string Content { get; set; } = null!;

		[Required]
		public string ArticleId { get; set; } = null!;
	}
}
