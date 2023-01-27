using Blog.Core.Abstractions;

namespace Blog.Core.Entities
{
	public class ArticlesCategories : BaseDeletableModel<string>
	{
		public ArticlesCategories()
		{
			Id = Guid.NewGuid().ToString();
			CreatedOn = DateTime.UtcNow;
		}

		public string ArticleId { get; set; } = null!;

		public virtual Article Article { get; set; } = null!;

		public string CategoryId { get; set; } = null!;

		public virtual Category Category { get; set; } = null!;

	}
}
