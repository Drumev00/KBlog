using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
	internal class SubCommentsDislikesConfiguration : IEntityTypeConfiguration<SubCommentsDislikes>
	{
		public void Configure(EntityTypeBuilder<SubCommentsDislikes> entity)
		{
			entity.HasKey(scd => new { scd.SubCommentId, scd.UserId });

			entity.HasOne(scd => scd.SubComment)
				.WithMany(sc => sc.SubCommentsDislikes)
				.HasForeignKey(scd => scd.SubCommentId)
				.IsRequired();

			entity.HasOne(scd => scd.User)
				.WithMany(u => u.SubCommentsDislikes)
				.HasForeignKey(scd => scd.UserId)
				.IsRequired();
		}
	}
}
