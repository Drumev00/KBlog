using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
	internal class ImageConfiguration : IEntityTypeConfiguration<Image>
	{
		public void Configure(EntityTypeBuilder<Image> entity)
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

			entity.Property(x => x.Picture)
				.HasColumnType("nvarchar(max)")
				.IsRequired();

			entity.HasOne(i => i.Article)
				.WithMany(a => a.Images)
				.HasForeignKey(i => i.ArticleId)
				.IsRequired();
		}
	}
}
