using Blog.Core.Entities;
using Blog.Infrastructure.Abstractions;
using static Blog.Core.Constants.Roles;

namespace Blog.Infrastructure.Seed
{
	internal class RolesSeeder : ISeeder
	{
		public async Task SeedAsync(ApplicationDbContext db)
		{
			if (!db.Roles.Any())
			{
				var role = new Role()
				{
					Name = AdminRole,
					NormalizedName = AdminRole.ToUpper(),
				};

				await db.AddAsync(role);
				await db.SaveChangesAsync();
			}
		}
	}
}
