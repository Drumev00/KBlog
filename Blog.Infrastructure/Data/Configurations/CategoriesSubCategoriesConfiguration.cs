using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Blog.Infrastructure.Data.Configurations
{
	internal class CategoriesSubCategoriesConfiguration : IEntityTypeConfiguration<CategoriesSubCategories>
	{
		public void Configure(EntityTypeBuilder<CategoriesSubCategories> entity)
		{
			entity.HasKey(csc => new { csc.CategoryId, csc.SubCategoryId });

			entity.HasOne(csc => csc.Category)
				.WithMany(c => c.CategoriesSubCategories)
				.HasForeignKey(csc => csc.CategoryId)
				.IsRequired();

			entity.HasOne(csc => csc.SubCategory)
				.WithMany(sc => sc.CategoriesSubCategories)
				.HasForeignKey(csc => csc.SubCategoryId)
				.IsRequired();
		}
	}
}
