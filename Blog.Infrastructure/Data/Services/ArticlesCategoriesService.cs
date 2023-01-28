using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data.Services
{
	public class ArticlesCategoriesService
	{
		private readonly ApplicationDbContext _dbContext;

		public ArticlesCategoriesService(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task AddAsync(string articleId, string categoryId)
		{
			var articleCategory = new Core.Entities.ArticlesCategories
			{
				ArticleId = articleId,
				CategoryId = categoryId
			};

			await _dbContext.ArticlesCategories.AddAsync(articleCategory);
			await _dbContext.SaveChangesAsync();

		}

		public async Task DeleteAsync(string articleId)
		{
			var articlesCategories = await _dbContext
				.ArticlesCategories
				.Where(ac => ac.ArticleId == articleId && !ac.IsDeleted)
				.ToListAsync();

			foreach (var articleCategory in articlesCategories)
			{
				articleCategory.IsDeleted = true;
				articleCategory.DeletedOn = DateTime.UtcNow;
			}

			await _dbContext.SaveChangesAsync();
		}
	}
}
