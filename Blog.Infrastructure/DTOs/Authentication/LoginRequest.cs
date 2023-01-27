using System.ComponentModel.DataAnnotations;

namespace Blog.Infrastructure.DTOs.Authentication
{
	public record LoginRequest
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; } = null!;

		[Required]
		[MinLength(6)]
		public string Password { get; set; } = null!;
	}
}
