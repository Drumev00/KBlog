namespace Blog.Infrastructure.DTOs.SubComments
{
	public class SubCommentsListingDTO
	{
		public string Id { get; set; }

		public string Content { get; set; }

		public int Like { get; set; }

		public int Dislike { get; set; }

		public UserSubCommentAuthorDTO User { get; set; }
	}
}
