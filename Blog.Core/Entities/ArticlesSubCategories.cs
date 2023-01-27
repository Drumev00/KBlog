using Blog.Core.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Entities
{
	public class ArticlesSubCategories : BaseDeletableModel<string>
	{
		public ArticlesSubCategories()
		{
			Id = Guid.NewGuid().ToString();
			CreatedOn = DateTime.UtcNow;
		}

		[Required]
		public string ArticleId { get; set; } = null!;

		public virtual Article Article { get; set; } = null!;

		[Required]
		public string SubCategoryId { get; set; } = null!;

		public virtual SubCategory SubCategory { get; set; } = null!;

	}
}
