using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
	internal class ArticleConfiguration : IEntityTypeConfiguration<Article>
	{
		public void Configure(EntityTypeBuilder<Article> entity)
		{
			entity.HasKey(x => x.Id);

			entity.Property(x => x.CreatedOn)
				.IsRequired();

			entity.Property(x => x.IsDeleted)
				.IsRequired();

			entity.Property(x => x.DeletedOn)
				.IsRequired(false);

			entity.Property(x => x.ModifiedOn)
				.IsRequired(false);

			entity.Property(x => x.Title)
				.HasMaxLength(120)
				.IsRequired();

			entity.Property(x => x.Content)
				.HasColumnType("nvarchar(max)")
				.IsRequired();

			entity.Property(x => x.TitlePicture)
				.HasColumnType("nvarchar(max)")
				.IsRequired();

			entity.Property(x => x.Likes)
				.IsRequired();

			entity.Property(x => x.Dislikes)
				.IsRequired();

			entity.HasOne(a => a.User)
				.WithMany(u => u.Articles)
				.HasForeignKey(a => a.UserId)
				.IsRequired();

			entity.HasMany(a => a.Images)
				.WithOne(i => i.Article)
				.HasForeignKey(i => i.ArticleId)
				.IsRequired();

			entity.HasMany(a => a.Comments)
				.WithOne(c => c.Article)
				.HasForeignKey(c => c.ArticleId)
				.IsRequired();

			entity.HasMany(a => a.ArticlesCategories)
				.WithOne(ac => ac.Article);

			entity.HasMany(a => a.ArticlesSubCategories)
				.WithOne(asc => asc.Article);

			entity.HasMany(a => a.ArticlesLikes)
				.WithOne(ac => ac.Article);

			entity.HasMany(a => a.ArticlesDislikes)
				.WithOne(ac => ac.Article);
		}
	}
}
