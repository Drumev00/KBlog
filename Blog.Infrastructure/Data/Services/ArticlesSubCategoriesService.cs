using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data.Services
{
	public class ArticlesSubCategoriesService
	{
		private readonly ApplicationDbContext _dbContext;

		public ArticlesSubCategoriesService(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task AddAsync(string articleId, string subCategoryId)
		{
			var articleSubCategory = new Core.Entities.ArticlesSubCategories
			{
				ArticleId = articleId,
				SubCategoryId = subCategoryId
			};

			await _dbContext.ArticlesSubCategories.AddAsync(articleSubCategory);
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(string articleId)
		{
			var articlesSubCategories = await _dbContext
				.ArticlesSubCategories
				.Where(asc => asc.ArticleId == articleId && !asc.IsDeleted)
				.ToListAsync();

			foreach (var articleSubCategory in articlesSubCategories)
			{
				articleSubCategory.IsDeleted = true;
				articleSubCategory.DeletedOn = DateTime.UtcNow;
			}

			await _dbContext.SaveChangesAsync();
		}
	}
}
