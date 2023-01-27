using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data.Services
{
	public class ArticlesCategoriesService
	{
		private readonly ApplicationDbContext dbContext;

		public ArticlesCategoriesService(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task AddAsync(string articleId, string categoryId)
		{
			var articleCategory = new Core.Entities.ArticlesCategories
			{
				ArticleId = articleId,
				CategoryId = categoryId
			};

			await dbContext.ArticlesCategories.AddAsync(articleCategory);
			await dbContext.SaveChangesAsync();

		}

		public async Task DeleteAsync(string articleId)
		{
			var articlesCategories = await dbContext
				.ArticlesCategories
				.Where(ac => ac.ArticleId == articleId && !ac.IsDeleted)
				.ToListAsync();

			foreach (var articleCategory in articlesCategories)
			{
				articleCategory.IsDeleted = true;
			}

			dbContext.ArticlesCategories.UpdateRange(articlesCategories);
		}
	}
}
