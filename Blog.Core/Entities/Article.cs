using Blog.Core.Abstractions;

namespace Blog.Core.Entities
{
	public class Article : BaseDeletableModel<string>
	{
		public Article()
		{
			Id = Guid.NewGuid().ToString();
			CreatedOn = DateTime.UtcNow;

			Images = new HashSet<Image>();
			ArticlesCategories = new HashSet<ArticlesCategories>();
			ArticlesLikes = new HashSet<ArticlesLikes>();
			ArticlesDislikes = new HashSet<ArticlesDislikes>();
			Comments = new HashSet<Comment>();
			ArticlesSubCategories = new HashSet<ArticlesSubCategories>();
		}

		public string Title { get; set; } = null!;

		public string Content { get; set; } = null!;

		public string TitlePicture { get; set; } = null!;

		public int Likes { get; set; }

		public int Dislikes { get; set; }

		public string UserId { get; set; } = null!;

		public virtual User User { get; set; } = null!;

		public virtual ICollection<Image> Images { get; set; }

		public virtual ICollection<ArticlesCategories> ArticlesCategories { get; set; }

		public virtual ICollection<ArticlesSubCategories> ArticlesSubCategories { get; set; }

		public virtual ICollection<ArticlesLikes> ArticlesLikes { get; set; }

		public virtual ICollection<ArticlesDislikes> ArticlesDislikes { get; set; }

		public virtual ICollection<Comment> Comments { get; set; }
	}
}
