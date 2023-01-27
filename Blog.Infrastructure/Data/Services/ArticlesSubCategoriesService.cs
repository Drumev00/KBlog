using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data.Services
{
	public class ArticlesSubCategoriesService
	{
		private readonly ApplicationDbContext dbContext;

		public ArticlesSubCategoriesService(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task AddAsync(string articleId, string subCategoryId)
		{
			var mappingRecord = await dbContext
				.ArticlesSubCategories
				.Where(asc => asc.ArticleId == articleId && asc.SubCategoryId == subCategoryId && !asc.IsDeleted)
				.FirstOrDefaultAsync();

			if (mappingRecord == null)
			{
				var articleSubCategory = new Core.Entities.ArticlesSubCategories
				{
					ArticleId = articleId,
					SubCategoryId = subCategoryId
				};

				await dbContext.ArticlesSubCategories.AddAsync(articleSubCategory);
				await dbContext.SaveChangesAsync();
			}

		}
	}
}
