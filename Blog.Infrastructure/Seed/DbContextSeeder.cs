using Blog.Infrastructure.Abstractions;

namespace Blog.Infrastructure.Seed
{
	public class DbContextSeeder : ISeeder
	{
		public async Task SeedAsync(ApplicationDbContext db)
		{
			if (db == null)
			{
				throw new ArgumentNullException(nameof(db));
			}

			var seeders = new List<ISeeder>
			{
				new RolesSeeder(),
				new DataSeeder(),
			};

			foreach (var seeder in seeders)
			{
				await seeder.SeedAsync(db);
				await db.SaveChangesAsync();
			}
		}
	}
}
