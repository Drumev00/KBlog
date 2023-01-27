using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Core.Entities.Configurations
{
	public class ArticlesSubCategoriesConfigurations : IEntityTypeConfiguration<ArticlesSubCategories>
	{
		public void Configure(EntityTypeBuilder<ArticlesSubCategories> builder)
		{
			builder.HasKey(asc => new { asc.ArticleId, asc.SubCategoryId });
		}
	}
}
