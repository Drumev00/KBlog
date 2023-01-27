using Blog.Core.Entities;
using Blog.Infrastructure.Abstractions;

namespace Blog.Infrastructure.Seed
{
	internal class DataSeeder : ISeeder
	{
		public async Task SeedAsync(ApplicationDbContext db)
		{
			if (!db.Categories.Any())
			{
				var categories = new List<Category>()
				{
					new Category()
					{
						Name= "Philosophy",
					},
					new Category()
					{
						Name= "Psychology",
					},
				};

				await db.Categories.AddRangeAsync(categories);
				await db.SaveChangesAsync();
			}
		}
	}
}
