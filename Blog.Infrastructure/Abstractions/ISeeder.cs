namespace Blog.Infrastructure.Abstractions
{
    public interface ISeeder
    {
        Task SeedAsync(ApplicationDbContext db);
    }
}
