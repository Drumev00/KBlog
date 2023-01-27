using System.ComponentModel.DataAnnotations;

namespace Blog.Infrastructure.DTOs.SubComments
{
	public class SubCommentPostRequest
	{
		[Required]
		public string Content { get; set; }

		[Required]
		public string CommentId { get; set; }
	}
}
