using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Core.Entities.Configurations
{
	public class SubCommentsLikesConfiguration : IEntityTypeConfiguration<SubCommentsLikes>
	{
		public void Configure(EntityTypeBuilder<SubCommentsLikes> builder)
		{
			builder.HasKey(sl => new { sl.SubCommentId, sl.UserId });
		}
	}
}
