using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
	internal class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
	{
		public void Configure(EntityTypeBuilder<SubCategory> entity)
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

			entity.Property(x => x.Name)
				.HasMaxLength(80)
				.IsRequired();

			entity.HasMany(sc => sc.ArticlesSubCategories)
				.WithOne(asc => asc.SubCategory);

			entity.HasMany(sc => sc.CategoriesSubCategories)
				.WithOne(asc => asc.SubCategory);
		}
	}
}
