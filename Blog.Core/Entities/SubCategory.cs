using Blog.Core.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Entities
{
    public class SubCategory : BaseDeletableModel<string>
	{
		public SubCategory()
		{
			Id = Guid.NewGuid().ToString();
			CreatedOn = DateTime.UtcNow;

			ArticlesSubCategories = new HashSet<ArticlesSubCategories>();
			CategoriesSubCategories = new HashSet<CategoriesSubCategories>();
		}

		public string Name { get; set; } = null!;

		public virtual ICollection<ArticlesSubCategories> ArticlesSubCategories { get; set; }


		public virtual ICollection<CategoriesSubCategories> CategoriesSubCategories { get; set; }
	}
}
