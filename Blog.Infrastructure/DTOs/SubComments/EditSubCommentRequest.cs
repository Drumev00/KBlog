namespace Blog.Infrastructure.DTOs.SubComments
{
	public record EditSubCommentRequest
	{
		public string SubCommentId { get; set; } = null!;

		public string NewContent { get; set; } = null!;
	}
}
