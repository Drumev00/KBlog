using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
	internal class ArticleSubCategoriesConfiguration : IEntityTypeConfiguration<ArticlesSubCategories>
	{
		public void Configure(EntityTypeBuilder<ArticlesSubCategories> entity)
		{
			entity.HasKey(asc => new { asc.ArticleId, asc.SubCategoryId });

			entity.HasOne(asc => asc.Article)
				.WithMany(a => a.ArticlesSubCategories)
				.HasForeignKey(asc => asc.ArticleId)
				.IsRequired();

			entity.HasOne(asc => asc.SubCategory)
				.WithMany(sc => sc.ArticlesSubCategories)
				.HasForeignKey(asc => asc.SubCategoryId)
				.IsRequired();
		}
	}
}
