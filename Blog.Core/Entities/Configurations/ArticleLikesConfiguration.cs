using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Core.Entities.Configurations
{
	public class ArticleLikesConfiguration : IEntityTypeConfiguration<ArticlesLikes>
	{
		public void Configure(EntityTypeBuilder<ArticlesLikes> builder)
		{
			builder.HasKey(al => new { al.UserId, al.ArticleId });
		}
	}
}
