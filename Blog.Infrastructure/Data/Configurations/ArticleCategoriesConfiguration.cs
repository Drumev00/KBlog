using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
	internal class ArticleCategoriesConfiguration : IEntityTypeConfiguration<ArticlesCategories>
	{
		public void Configure(EntityTypeBuilder<ArticlesCategories> entity)
		{
			entity.HasKey(ac => new { ac.ArticleId, ac.CategoryId });

			entity.HasOne(ac => ac.Article)
				.WithMany(a => a.ArticlesCategories)
				.HasForeignKey(ac => ac.ArticleId)
				.IsRequired();

			entity.HasOne(ac => ac.Category)
				.WithMany(c => c.ArticlesCategories)
				.HasForeignKey(ac => ac.CategoryId)
				.IsRequired();
		}
	}
}
