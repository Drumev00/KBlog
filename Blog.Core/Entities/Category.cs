using System.ComponentModel.DataAnnotations;
using Blog.Core.Abstractions;

namespace Blog.Core.Entities
{
	public class Category : BaseDeletableModel<string>
	{
		public Category()
		{
			Id = Guid.NewGuid().ToString();
			CreatedOn = DateTime.UtcNow;

			ArticlesCategories = new HashSet<ArticlesCategories>();
			CategoriesSubCategories = new HashSet<CategoriesSubCategories>();
		}

		public string Name { get; set; } = null!;

		public virtual ICollection<ArticlesCategories> ArticlesCategories { get; set; }

		public virtual ICollection<CategoriesSubCategories> CategoriesSubCategories { get; set; }
	}
}
