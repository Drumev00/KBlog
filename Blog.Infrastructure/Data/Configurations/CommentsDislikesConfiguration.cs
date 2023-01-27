using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
	internal class CommentsDislikesConfiguration : IEntityTypeConfiguration<CommentsDislikes>
	{
		public void Configure(EntityTypeBuilder<CommentsDislikes> entity)
		{
			entity.HasKey(cd => new { cd.CommentId, cd.UserId });

			entity.HasOne(cd => cd.Comment)
				.WithMany(c => c.CommentsDislikes)
				.HasForeignKey(cd => cd.CommentId)
				.IsRequired();

			entity.HasOne(cd => cd.User)
				.WithMany(u => u.CommentsDislikes)
				.HasForeignKey(cd => cd.UserId)
				.IsRequired();
		}
	}
}
