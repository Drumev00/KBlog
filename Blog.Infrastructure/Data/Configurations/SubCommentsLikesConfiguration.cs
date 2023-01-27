using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
	internal class SubCommentsLikesConfiguration : IEntityTypeConfiguration<SubCommentsLikes>
	{
		public void Configure(EntityTypeBuilder<SubCommentsLikes> entity)
		{
			entity.HasKey(scd => new { scd.SubCommentId, scd.UserId });

			entity.HasOne(scl => scl.SubComment)
				.WithMany(sc => sc.SubCommentsLikes)
				.HasForeignKey(scl => scl.SubCommentId)
				.IsRequired();

			entity.HasOne(scl => scl.User)
				.WithMany(u => u.SubCommentsLikes)
				.HasForeignKey(scl => scl.UserId)
				.IsRequired();
		}
	}
}
