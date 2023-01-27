using System.ComponentModel.DataAnnotations;

namespace Blog.Infrastructure.DTOs.Authentication
{
	public record RegisterRequest()
	{
		[EmailAddress]
		public string Email { get; set; } = null!;

		[Required]
		[MaxLength(50)]
		public string Username { get; set; } = null!;

		[Required]
		public string Password { get; set; } = null!;

		[Required]
		public string ConfirmPassword { get; set; } = null!;
	}
}
