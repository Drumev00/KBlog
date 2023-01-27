using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Blog.Infrastructure.DTOs.Articles
{
	public class CreateArticleRequest
	{
		[Required]
		[MinLength(3)]
		[MaxLength(120)]
		public string Title { get; set; } = null!;

		[Required]
		public string Content { get; set; } = null!;

		[Required]
		public IFormFile TitlePicture { get; set; } = null!;

		public IEnumerable<string> Categories { get; set; }
	}
}
