using System.ComponentModel.DataAnnotations;
using Blog.Core.Abstractions;

namespace Blog.Core.Entities
{
	public class CategoriesSubCategories : BaseDeletableModel<string>
	{
		public CategoriesSubCategories()
		{
			Id = Guid.NewGuid().ToString();
			CreatedOn = DateTime.UtcNow;
		}

		[Required]
		public string CategoryId { get; set; } = null!;

		public virtual Category Category { get; set; } = null!;

		[Required]
		public string SubCategoryId { get; set; } = null!;

		public virtual SubCategory SubCategory { get; set; } = null!;
	}
}
