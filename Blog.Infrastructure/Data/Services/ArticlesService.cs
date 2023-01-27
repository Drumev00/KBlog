using Blog.Core.Entities;
using Blog.Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using Ganss.Xss;
using Blog.Infrastructure.DTOs.Articles;
using Blog.Infrastructure.DTOs.Comments;
using Blog.Infrastructure.DTOs.Users;
using Blog.Infrastructure.DTOs.SubComments;

namespace Blog.Infrastructure.Data.Services
{
	public class ArticlesService
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly ArticlesCategoriesService _articlesCategoriesService;
		private readonly ImagesService _imagesService;

		public ArticlesService(ApplicationDbContext dbContext,
			ArticlesCategoriesService articlesCategoriesService,
			ImagesService imagesService)
		{
			this._dbContext = dbContext;
			this._articlesCategoriesService = articlesCategoriesService;
			this._imagesService = imagesService;
		}

		public async Task<string> CreateArticleAsync(string userId, CreateArticleRequest request)
		{
			var sanitizer = new HtmlSanitizer();

			var proccessedPic = await _imagesService.ImageToByteArrayAsync(request.TitlePicture);

			var article = new Article
			{
				UserId = userId,
				Content = sanitizer.Sanitize(request.Content),
				TitlePicture = proccessedPic,
				Title = request.Title,
			};

			await _dbContext.Articles.AddAsync(article);
			await _dbContext.SaveChangesAsync();

			if (request.Categories != null)
			{
				foreach (var category in request.Categories)
				{
					await _articlesCategoriesService.AddAsync(article.Id, category);
				}
			}

			return article.Id;
		}

		public async Task<string> DeleteAsync(string articleId)
		{
			var article = await _dbContext
				.Articles
				.Where(a => a.Id == articleId && !a.IsDeleted)
				.FirstOrDefaultAsync();

			article.IsDeleted = true;
			_dbContext.Articles.Update(article);

			await _articlesCategoriesService.DeleteAsync(articleId);
			await _dbContext.SaveChangesAsync();

			return article.Id;
		}

		public async Task<IEnumerable<ArticleListingDTO>> GetAllBlogArticlesAsync()
		{
			var articles = await _dbContext
				.Articles
				.AsNoTracking()
				.Where(a => !a.IsDeleted)
				.Select(a => new ArticleListingDTO
				{
					ArticleId = a.Id,
					Title = a.Title,
					TitlePicture = a.TitlePicture,
					PartOfContent = a.Content.Substring(0, 150) + "...",
					CreatedOn = a.CreatedOn.ToString("dd/MM/yyyy")
				})
				.ToListAsync();

			return articles;
		}

		public async Task<IEnumerable<ArticleListingDTO>> GetChosenArticlesByTopicAsync(string topic)
		{
			if (string.IsNullOrWhiteSpace(topic))
			{
				throw new ItemNotFoundException("No such topic exists.");
			}

			// Maybe Include() Categories as well..
			var articles = await _dbContext
				.Articles
				.Include(a => a.ArticlesCategories)
				.Where(a => a.ArticlesCategories.Any(ac => ac.ArticleId == a.Id && ac.Category.Name == topic && !ac.IsDeleted))
				.Select(a => new ArticleListingDTO
				{
					ArticleId = a.Id,
					Title = a.Title,
					TitlePicture = a.TitlePicture,
					PartOfContent = a.Content.Substring(0, 150) + "...",
					CreatedOn = a.CreatedOn.ToString("dd/MM/yyyy"),
					Topic = topic,
				})
				.ToListAsync();

			if (articles == null || articles.Count == 0)
			{
				throw new ItemNotFoundException("No such article exists.");
			}

			return articles;
		}

		public async Task<ArticleDetailsDTO> GetDetailsAsync(string articleId)
		{
			//var comments = await this.commentsService.GetAllAsync(articleId);


			var result = await _dbContext
				.Articles
				.Include(a => a.User)
				.Include(a => a.Comments)
				.ThenInclude(c => c.SubComments)
				.ThenInclude(c => c.User)
				.Where(a => a.Id == articleId && !a.IsDeleted)
				.Select(a => new ArticleDetailsDTO
				{
					Id = a.Id,
					Title = a.Title,
					TitlePicture = a.TitlePicture,
					Content = a.Content,
					CreatedOn = a.CreatedOn.ToString("dd/MM/yyyy"),
					LikesDislikes = new ArticleRateDTO
					{
						Likes = a.Likes,
						Dislikes = a.Dislikes
					},
					User = new UserArticleAuthorModel
					{
						Username = a.User.UserName,
						Email = a.User.Email,
						ProfilePic = a.User.ProfilePic
					},
					Comments = a.Comments.Select(c => new CommentsListingDTO
					{
						Id = c.Id,
						Content = c.Content,
						Like = c.Likes,
						Dislike = c.Dislikes,
						User = new UserCommentModel
						{
							UserId = c.UserId,
							Username = c.User.UserName,
							Email = c.User.Email,
							ProfilePic = c.User.ProfilePic,
						},
						SubComments = c.SubComments.Select(sc => new SubCommentsListingDTO
						{
							Id = sc.Id,
							Content = sc.Content,
							Like = sc.Likes,
							Dislike = sc.Dislikes,
							User = new UserSubCommentAuthorDTO
							{
								UserId = sc.UserId,
								Username = sc.User.UserName,
								Email = sc.User.Email,
								ProfilePic = sc.User.ProfilePic,
							}

						})
					}),
				})
				.FirstOrDefaultAsync();


			return result;
		}
	}
}
