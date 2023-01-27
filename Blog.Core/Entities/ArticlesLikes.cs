using System.ComponentModel.DataAnnotations;
using Blog.Core.Abstractions;

namespace Blog.Core.Entities
{
	public class ArticlesLikes : BaseModel<string>
	{
		public ArticlesLikes()
		{
			Id = Guid.NewGuid().ToString();
			CreatedOn = DateTime.UtcNow;
		}

		[Required]
		public string ArticleId { get; set; } = null!;

		public virtual Article Article { get; set; } = null!;

		[Required]
		public string UserId { get; set; } = null!;

		public virtual User User { get; set; } = null!;

	}
}
