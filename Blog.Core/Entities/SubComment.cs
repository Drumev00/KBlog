using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blog.Core.Abstractions;

namespace Blog.Core.Entities
{
	public class SubComment : BaseDeletableModel<string>
	{
		public SubComment()
		{
			Id = Guid.NewGuid().ToString();
			CreatedOn = DateTime.UtcNow;

			SubCommentsLikes = new HashSet<SubCommentsLikes>();
			SubCommentsDislikes = new HashSet<SubCommentsDislikes>();
		}

		public string Content { get; set; } = null!;

		public int Likes { get; set; }

		public int Dislikes { get; set; }

		public string UserId { get; set; } = null!;

		public virtual User User { get; set; } = null!;

		public string RootCommentId { get; set; } = null!;

		public virtual Comment Comment { get; set; } = null!;

		public virtual ICollection<SubCommentsLikes> SubCommentsLikes { get; set; }

		public virtual ICollection<SubCommentsDislikes> SubCommentsDislikes { get; set; }
	}
}
