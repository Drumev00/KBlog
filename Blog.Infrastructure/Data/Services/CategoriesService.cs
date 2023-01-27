using Blog.Core.Entities;
using Blog.Infrastructure.DTOs.Categories;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data.Services
{
	public class CategoriesService
	{
		private readonly ApplicationDbContext dbContext;

		public CategoriesService(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
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
			if (!string.IsNullOrWhiteSpace(name))
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
				return "Invalid category name!";
			}
		}
	}
}
