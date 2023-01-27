using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Core.Entities.Configurations
{
	public class ArticlesCategoriesConfiguration : IEntityTypeConfiguration<ArticlesCategories>
	{
		public void Configure(EntityTypeBuilder<ArticlesCategories> builder)
		{
			builder.HasKey(ac => new { ac.ArticleId, ac.CategoryId });
		}
	}
}
