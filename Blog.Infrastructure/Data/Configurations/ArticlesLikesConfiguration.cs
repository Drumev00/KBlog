using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
	internal class ArticlesLikesConfiguration : IEntityTypeConfiguration<ArticlesLikes>
	{
		public void Configure(EntityTypeBuilder<ArticlesLikes> entity)
		{
			entity.HasKey(al => new { al.ArticleId, al.UserId });

			entity.HasOne(al => al.Article)
				.WithMany(a => a.ArticlesLikes)
				.HasForeignKey(al => al.ArticleId)
				.IsRequired();

			entity.HasOne(al => al.User)
				.WithMany(u => u.ArticlesLikes)
				.HasForeignKey(al => al.UserId)
				.IsRequired();
		}
	}
}
