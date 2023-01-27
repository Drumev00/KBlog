using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
	internal class SubCommentConfiguration : IEntityTypeConfiguration<SubComment>
	{
		public void Configure(EntityTypeBuilder<SubComment> entity)
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

			entity.HasOne(sc => sc.User)
				.WithMany(u => u.SubComments)
				.HasForeignKey(sc => sc.UserId)
				.IsRequired();

			entity.HasOne(sc => sc.Comment)
				.WithMany(c => c.SubComments)
				.HasForeignKey(sc => sc.RootCommentId)
				.IsRequired();

			entity.HasMany(sc => sc.SubCommentsLikes)
				.WithOne(scl => scl.SubComment);

			entity.HasMany(sc => sc.SubCommentsDislikes)
				.WithOne(scl => scl.SubComment);
		}
	}
}
