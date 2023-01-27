using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
	internal class ArticlesDislikesConfiguration : IEntityTypeConfiguration<ArticlesDislikes>
	{
		public void Configure(EntityTypeBuilder<ArticlesDislikes> entity)
		{
			entity.HasKey(ad => new { ad.ArticleId, ad.UserId });

			entity.HasOne(ad => ad.Article)
				.WithMany(a => a.ArticlesDislikes)
				.HasForeignKey(ad => ad.ArticleId)
				.IsRequired();

			entity.HasOne(ad => ad.User)
				.WithMany(u => u.ArticlesDislikes)
				.HasForeignKey(ad => ad.UserId)
				.IsRequired();
		}
	}
}
