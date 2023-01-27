using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
	internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> entity)
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

			entity.Property(x => x.Content)
				.HasMaxLength(4000)
				.IsRequired();

			entity.Property(x => x.Likes)
				.IsRequired();

			entity.Property(x => x.Dislikes)
				.IsRequired();

			entity.HasOne(c => c.User)
				.WithMany(u => u.Comments)
				.HasForeignKey(c => c.UserId)
				.IsRequired();

			entity.HasOne(c => c.Article)
				.WithMany(a => a.Comments)
				.HasForeignKey(c => c.ArticleId)
				.IsRequired();

			entity.HasMany(c => c.SubComments)
				.WithOne(sc => sc.Comment)
				.HasForeignKey(sc => sc.RootCommentId)
				.IsRequired();
		}
	}
}
