using Blog.Core.Entities;
using Microsoft.Extensions.Logging;

namespace Blog.Infrastructure.Data.Services
{
	public class SubCategoriesService
	{
		private readonly ApplicationDbContext dbContext;
		private readonly ILogger<SubCategoriesService> _logger;

		public SubCategoriesService(ApplicationDbContext dbContext, ILogger<SubCategoriesService> logger)
		{
			this.dbContext = dbContext;
			this._logger = logger;
		}

		public async Task<string> CreateAsync(string name, IEnumerable<string> categoryIds)
		{
			var errorMessage = string.Empty;
			if (string.IsNullOrWhiteSpace(name) || name.Length > 80)
			{
				errorMessage = "Inappropriate name.";
				_logger.LogError(errorMessage);
				return errorMessage;
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
