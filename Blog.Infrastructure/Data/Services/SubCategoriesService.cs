using Blog.Core.Entities;

namespace Blog.Infrastructure.Data.Services
{
	public class SubCategoriesService
	{
		private readonly ApplicationDbContext dbContext;

		public SubCategoriesService(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<string> CreateAsync(string name, IEnumerable<string> categoryIds)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return "Inappropriate name.";
			}

			var subCategory = new SubCategory()
			{
				Name = name,
			};
			await dbContext.SubCategories.AddAsync(subCategory);

			var allMappings = new List<CategoriesSubCategories>();

			foreach (var categoryId in categoryIds)
			{
				allMappings.Add(new CategoriesSubCategories
				{
					CategoryId = categoryId,
					SubCategoryId = subCategory.Id
				});
			}

			await dbContext.CategoriesSubCategories.AddRangeAsync(allMappings);
			await dbContext.SaveChangesAsync();

			return subCategory.Id;
		}

		//public async Task<IEnumerable<SubCategoriesListingModel>> GetAllAsync()
		//{
		//	return await this.dbContext
		//		.SubCategories
		//		.Select(sc => new SubCategoriesListingModel
		//		{
		//			Id = sc.Id,
		//			CategoryId = sc.CategoriesSubCategories.CategoryId,
		//			CategoryName = sc.Category.Name,
		//			Name = sc.Name
		//		})
		//		.ToListAsync();
		//}
	}
}
