using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
	internal class CommentsLikesConfiguration : IEntityTypeConfiguration<CommentsLikes>
	{
		public void Configure(EntityTypeBuilder<CommentsLikes> entity)
		{
			entity.HasKey(cd => new { cd.CommentId, cd.UserId });

			entity.HasOne(cd => cd.Comment)
				.WithMany(c => c.CommentsLikes)
				.HasForeignKey(cd => cd.CommentId)
				.IsRequired();

			entity.HasOne(cd => cd.User)
				.WithMany(u => u.CommentsLikes)
				.HasForeignKey(cd => cd.UserId)
				.IsRequired();
		}
	}
}
