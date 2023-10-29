using Microsoft.AspNetCore.Identity;
using Blog.Core.Abstractions;

namespace Blog.Core.Entities
{
	public class User : IdentityUser, IAuditInfo
	{
		public User()
		{
			Id = Guid.NewGuid().ToString();
			CreatedOn = DateTime.UtcNow;

			Articles = new HashSet<Article>();
			ArticlesLikes = new HashSet<ArticlesLikes>();
			ArticlesDislikes = new HashSet<ArticlesDislikes>();
			Comments = new HashSet<Comment>();
			CommentsLikes = new HashSet<CommentsLikes>();
			CommentsDislikes = new HashSet<CommentsDislikes>();
			SubComments = new HashSet<SubComment>();
			SubCommentsLikes = new HashSet<SubCommentsLikes>();
			SubCommentsDislikes = new HashSet<SubCommentsDislikes>();
			RefreshTokens = new HashSet<RefreshToken>();
		}

		public DateTime CreatedOn { get; set; }
		public DateTime? ModifiedOn { get; set; }

		public string? ProfilePic { get; set; }

		public virtual ICollection<Article> Articles { get; set; }

		public virtual ICollection<ArticlesLikes> ArticlesLikes { get; set; }

		public virtual ICollection<ArticlesDislikes> ArticlesDislikes { get; set; }

		public virtual ICollection<Comment> Comments { get; set; }

		public virtual ICollection<CommentsLikes> CommentsLikes { get; set; }

		public virtual ICollection<CommentsDislikes> CommentsDislikes { get; set; }

		public virtual ICollection<SubComment> SubComments { get; set; }

		public virtual ICollection<SubCommentsLikes> SubCommentsLikes { get; set; }

		public virtual ICollection<SubCommentsDislikes> SubCommentsDislikes { get; set; }

		public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
	}
}
