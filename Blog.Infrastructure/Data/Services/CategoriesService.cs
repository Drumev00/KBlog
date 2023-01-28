using Blog.Core.Entities;
using Blog.Infrastructure.DTOs.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Blog.Infrastructure.Data.Services
{
	public class CategoriesService
	{
		private readonly ApplicationDbContext dbContext;
		private readonly ILogger<CategoriesService> _logger;

		public CategoriesService(ApplicationDbContext dbContext, ILogger<CategoriesService> logger)
		{
			this.dbContext = dbContext;
			this._logger = logger;
		}

		public async Task<IEnumerable<CategoriesListingDTO>> GetAllAsync()
		{
			return await dbContext
				.Categories
				.AsNoTracking()
				.Where(c => !c.IsDeleted)
				.OrderBy(x => x.Name)
				.Select(c => new CategoriesListingDTO
				{
					Id = c.Id,
					Name = c.Name
				})
				.ToListAsync();
		}

		public async Task<string> CreateAsync(string name)
		{
			var errorMessage = string.Empty;

			if (!string.IsNullOrWhiteSpace(name) || name.Length <= 80)
			{
				var category = new Category
				{
					Name = name
				};

				await dbContext.Categories.AddAsync(category);
				await dbContext.SaveChangesAsync();

				return category.Id;
			}
			else
			{
				errorMessage = "Invalid category name!";
				_logger.LogError(errorMessage);
				return errorMessage;
			}
		}
	}
}
