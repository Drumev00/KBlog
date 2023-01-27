using Blog.Infrastructure.Data.Services;
using Blog.Infrastructure.Data.Services.Rate.Articles;
using Blog.Infrastructure.DTOs.Articles;
using Blog.Infrastructure.DTOs.Categories;
using Blog.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
	public class ArticlesController : MyController
	{
		private readonly ArticlesService _articlesService;
		private readonly CategoriesService _categoriesService;
		private readonly ArticlesRateService _rateService;

		public ArticlesController(
			ArticlesService articlesService,
			CategoriesService categoriesService,
			ArticlesRateService rateService)
		{
			_articlesService = articlesService;
			_categoriesService = categoriesService;
			_rateService = rateService;
		}

		// Admin, User
		[HttpGet]
		[AllowAnonymous]
		public Task<IEnumerable<ArticleListingDTO>> Blog()
		{
			return _articlesService.GetAllBlogArticlesAsync();
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public Task<string> Create(CreateArticleRequest model)
		{
			var userId = User.GetId();

			return _articlesService.CreateArticleAsync(userId, model);
		}

		[Authorize(Roles = "Admin")]
		[HttpGet("create")]
		public Task<IEnumerable<CategoriesListingDTO>> GetForm()
		{
			return _categoriesService.GetAllAsync();
		}


		// Admin, User
		[HttpGet("byTopic")]
		public Task<IEnumerable<ArticleListingDTO>> Topics([FromQuery(Name = "topic")] string topic)
		{
			return _articlesService.GetChosenArticlesByTopicAsync(topic);
		}



		// Admin, User
		[HttpGet("{articleId}")]
		public Task<ArticleDetailsDTO> Details([FromQuery(Name = "articleId")] string articleId)
		{
			return _articlesService.GetDetailsAsync(articleId);
		}

		[HttpPost("like")]
		public Task<int> Like(string articleId)
		{
			var userId = User.GetId();

			return _rateService.LikeAsync(userId, articleId);
		}

		[HttpPost("dislike")]
		public Task<int> Dislike(string articleId)
		{
			var userId = User.GetId();

			return _rateService.DislikeAsync(userId, articleId);
		}

		[HttpDelete("{articleId}")]
		[Authorize(Roles = "Admin")]
		public Task Delete([FromQuery(Name = "articleId")] string articleId)
		{
			return _articlesService.DeleteAsync(articleId);
		}
	}
}
