using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
	internal class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> entity)
		{
			entity.HasKey(x => x.Id);

			entity.Property(x => x.CreatedOn)
				.IsRequired();

			entity.Property(x => x.ModifiedOn)
				.IsRequired(false);

			entity.Property(x => x.ProfilePic)
				.HasColumnType("nvarchar(max)")
				.IsRequired(false);

			entity.HasMany(u => u.Articles)
				.WithOne(a => a.User);

			entity.HasMany(u => u.ArticlesLikes)
				.WithOne(al => al.User);

			entity.HasMany(u => u.ArticlesDislikes)
				.WithOne(ad => ad.User);

			entity.HasMany(u => u.Comments)
				.WithOne(c => c.User);

			entity.HasMany(u => u.CommentsLikes)
				.WithOne(cl => cl.User);

			entity.HasMany(u => u.CommentsDislikes)
				.WithOne(cd => cd.User);

			entity.HasMany(u => u.SubComments)
				.WithOne(sc => sc.User);

			entity.HasMany(u => u.SubCommentsLikes)
				.WithOne(scl => scl.User);

			entity.HasMany(u => u.SubCommentsDislikes)
				.WithOne(scd => scd.User);
		}
	}
}
