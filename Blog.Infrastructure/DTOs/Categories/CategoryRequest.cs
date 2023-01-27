using System.ComponentModel.DataAnnotations;

namespace Blog.Infrastructure.DTOs.Categories
{
	public class CategoryRequest
	{
		[Required]
		[MaxLength(60, ErrorMessage = "Category's name cannot be more than 60 characters long.")]
		public string Name { get; set; }
	}
}
