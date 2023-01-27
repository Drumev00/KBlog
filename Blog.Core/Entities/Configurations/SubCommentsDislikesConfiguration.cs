using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Core.Entities.Configurations
{
	public class SubCommentsDislikesConfiguration : IEntityTypeConfiguration<SubCommentsDislikes>
	{
		public void Configure(EntityTypeBuilder<SubCommentsDislikes> builder)
		{
			builder.HasKey(sd => new { sd.SubCommentId, sd.UserId });
		}
	}
}
