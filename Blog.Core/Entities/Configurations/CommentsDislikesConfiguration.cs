using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Core.Entities.Configurations
{
	public class CommentsDislikesConfiguration : IEntityTypeConfiguration<CommentsDislikes>
	{
		public void Configure(EntityTypeBuilder<CommentsDislikes> builder)
		{
			builder.HasKey(cd => new { cd.CommentId, cd.UserId });
		}
	}
}
