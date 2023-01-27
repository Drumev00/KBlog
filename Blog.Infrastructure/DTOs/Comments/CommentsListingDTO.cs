using Blog.Infrastructure.DTOs.SubComments;

namespace Blog.Infrastructure.DTOs.Comments
{
	public class CommentsListingDTO
	{
		public string Id { get; set; }

		public string Content { get; set; }

		public int Like { get; set; }

		public int Dislike { get; set; }

		public UserCommentModel User { get; set; }

		public IEnumerable<SubCommentsListingDTO> SubComments { get; set; }
	}
}
