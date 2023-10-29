using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Blog.Core.Entities;

namespace Blog.Infrastructure
{
	public class ApplicationDbContext : IdentityDbContext<User, Role, string>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Article> Articles { get; set; }

		public DbSet<Image> Images { get; set; }

		public DbSet<Category> Categories { get; set; }

		public DbSet<ArticlesCategories> ArticlesCategories { get; set; }

		public DbSet<ArticlesLikes> ArticlesLikes { get; set; }

		public DbSet<ArticlesDislikes> ArticlesDislikes { get; set; }

		public DbSet<Comment> Comments { get; set; }

		public DbSet<CommentsLikes> CommentsLikes { get; set; }

		public DbSet<CommentsDislikes> CommentsDislikes { get; set; }

		public DbSet<SubComment> SubComments { get; set; }

		public DbSet<SubCommentsLikes> SubCommentsLikes { get; set; }

		public DbSet<SubCommentsDislikes> SubCommentsDislikes { get; set; }

		public DbSet<SubCategory> SubCategories { get; set; }

		public DbSet<ArticlesSubCategories> ArticlesSubCategories { get; set; }

		public DbSet<CategoriesSubCategories> CategoriesSubCategories { get; set; }

		public DbSet<RefreshToken> RefreshTokens { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
		}
	}
}
