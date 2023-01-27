using Blog.Infrastructure.Data.Services;
using Blog.Infrastructure.DTOs.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
	public class CategoriesController : MyController
	{
		private readonly CategoriesService _categoriesService;

		public CategoriesController(CategoriesService categoriesService)
		{
			_categoriesService = categoriesService;
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public Task<string> Create(CategoryRequest model)
		{
			return _categoriesService.CreateAsync(model.Name);
		}
	}
}
