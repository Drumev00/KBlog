using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
	internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> entity)
		{
			entity.HasKey(x => x.Id);

			entity.Property(x => x.CreatedOn)
				.IsRequired();

			entity.Property(x => x.Name)
				.HasMaxLength(80)
				.IsRequired();

			entity.Property(x => x.IsDeleted)
				.IsRequired();

			entity.Property(x => x.DeletedOn)
				.IsRequired(false);

			entity.Property(x => x.ModifiedOn)
				.IsRequired(false);
		}
	}
}
