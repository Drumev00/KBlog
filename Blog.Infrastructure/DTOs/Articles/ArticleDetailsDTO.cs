using Blog.Infrastructure.DTOs.Comments;
using Blog.Infrastructure.DTOs.Users;

namespace Blog.Infrastructure.DTOs.Articles
{
	public class ArticleDetailsDTO
	{
		public string Id { get; set; }

		public string Title { get; set; }

		public string TitlePicture { get; set; }

		public string Content { get; set; }

		public ArticleRateDTO LikesDislikes { get; set; }

		public string CreatedOn { get; set; }

		public UserArticleAuthorModel User { get; set; }

		public IEnumerable<CommentsListingDTO> Comments { get; set; }
	}
}
