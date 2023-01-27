using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Core.Entities.Configurations
{
	public class ArticleDislikesConfiguration : IEntityTypeConfiguration<ArticlesDislikes>
	{
		public void Configure(EntityTypeBuilder<ArticlesDislikes> builder)
		{
			builder.HasKey(ad => new { ad.ArticleId, ad.UserId });
		}
	}
}
