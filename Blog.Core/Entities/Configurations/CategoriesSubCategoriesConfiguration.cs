using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Core.Entities.Configurations
{
	public class CategoriesSubCategoriesConfiguration : IEntityTypeConfiguration<CategoriesSubCategories>
	{
		public void Configure(EntityTypeBuilder<CategoriesSubCategories> builder)
		{
			builder.HasKey(csc => new { csc.CategoryId, csc.SubCategoryId });
		}
	}
}
