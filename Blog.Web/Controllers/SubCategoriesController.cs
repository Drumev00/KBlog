using Blog.Infrastructure.Data.Services;
using Blog.Infrastructure.DTOs.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
	public class SubCategoriesController : MyController
	{
		private readonly SubCategoriesService _subCategoriesService;
		private readonly CategoriesService _categoriesService;

		public SubCategoriesController(
			SubCategoriesService subCategoriesService,
			CategoriesService categoriesService)
		{
			_subCategoriesService = subCategoriesService;
			_categoriesService = categoriesService;
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public Task<IEnumerable<CategoriesListingDTO>> Create()
		{
			return _categoriesService.GetAllAsync();
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public Task<string> Create(string name, IEnumerable<string> categories)
		{
			return _subCategoriesService.CreateAsync(name, categories);
		}
	}
}
