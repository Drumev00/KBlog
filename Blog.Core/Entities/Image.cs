using System.ComponentModel.DataAnnotations;
using Blog.Core.Abstractions;

namespace Blog.Core.Entities
{
	public class Image : BaseDeletableModel<string>
	{
		public Image()
		{
			Id = Guid.NewGuid().ToString();
			CreatedOn = DateTime.UtcNow;
		}

		public string Picture { get; set; } = null!;

		public string ArticleId { get; set; } = null!;

		public virtual Article Article { get; set; } = null!;

	}
}
