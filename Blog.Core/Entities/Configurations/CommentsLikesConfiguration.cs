using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Core.Entities.Configurations
{
	public class CommentsLikesConfiguration : IEntityTypeConfiguration<CommentsLikes>
	{
		public void Configure(EntityTypeBuilder<CommentsLikes> builder)
		{
			builder.HasKey(cl => new { cl.CommentId, cl.UserId });
		}
	}
}
