using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data.Services.Rate.Articles
{
	public class ArticlesRateService
	{
		private readonly ApplicationDbContext dbContext;

		public ArticlesRateService(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<int> LikeAsync(string articleId, string userId)
		{
			var article = await dbContext
				.Articles
				.Include(a => a.ArticlesLikes)
				.Include(a => a.ArticlesDislikes)
				.FirstOrDefaultAsync(a => a.Id == articleId && !a.IsDeleted);

			var articleLike = new ArticlesLikes
			{
				ArticleId = article.Id,
				UserId = userId,
			};

			if (!article.ArticlesLikes.Any(al => al.UserId == userId && al.ArticleId == articleId))
			{
				await dbContext.ArticlesLikes.AddAsync(articleLike);

				article.Likes++;
			}
			else if (article.ArticlesDislikes.Any(ad => ad.UserId == userId && ad.ArticleId == articleId))
			{
				var dislikeToRemove = article.ArticlesDislikes.FirstOrDefault(ad => ad.UserId == userId && ad.ArticleId == articleId);
				dbContext.ArticlesDislikes.Remove(dislikeToRemove);
				article.Dislikes--;

				await dbContext.ArticlesLikes.AddAsync(articleLike);
				article.Likes++;
			}

			await dbContext.SaveChangesAsync();

			return article.Likes;
		}

		public async Task<int> DislikeAsync(string articleId, string userId)
		{
			var article = await dbContext
				.Articles
				.Include(a => a.ArticlesLikes)
				.Include(a => a.ArticlesDislikes)
				.FirstOrDefaultAsync(a => a.Id == articleId && !a.IsDeleted);

			var articleDislike = new ArticlesDislikes
			{
				ArticleId = article.Id,
				UserId = userId,
			};

			if (!article.ArticlesDislikes.Any(ad => ad.ArticleId == articleId && ad.UserId == userId))
			{
				await dbContext.ArticlesDislikes.AddAsync(articleDislike);

				article.Dislikes++;
			}
			else if (article.ArticlesLikes.Any(al => al.ArticleId == articleId && al.UserId == userId))
			{
				var likeToRemove = article.ArticlesLikes.FirstOrDefault(ad => ad.UserId == userId && ad.ArticleId == articleId);
				dbContext.ArticlesLikes.Remove(likeToRemove);
				article.Likes--;

				await dbContext.ArticlesDislikes.AddAsync(articleDislike);
				article.Dislikes++;
			}

			await dbContext.SaveChangesAsync();

			return article.Dislikes;
		}
	}
}
