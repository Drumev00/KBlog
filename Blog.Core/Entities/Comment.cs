using Blog.Core.Abstractions;

namespace Blog.Core.Entities
{
	public class Comment : BaseDeletableModel<string>
	{
		public Comment()
		{
			Id = Guid.NewGuid().ToString();
			CreatedOn = DateTime.UtcNow;

			CommentsLikes = new HashSet<CommentsLikes>();
			CommentsDislikes = new HashSet<CommentsDislikes>();
			SubComments = new HashSet<SubComment>();
		}

		public string Content { get; set; } = null!;

		public int Likes { get; set; }

		public int Dislikes { get; set; }

		public string UserId { get; set; } = null!;

		public virtual User User { get; set; } = null!;

		public string ArticleId { get; set; } = null!;

		public virtual Article Article { get; set; } = null!;

		public virtual ICollection<CommentsLikes> CommentsLikes { get; set; }

		public virtual ICollection<CommentsDislikes> CommentsDislikes { get; set; }

		public virtual ICollection<SubComment> SubComments { get; set; }
	}
}
