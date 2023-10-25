using System.ComponentModel.DataAnnotations;

namespace Blog.Infrastructure.DTOs.Authentication
{
	public record RegisterRequest()
	{
		[EmailAddress]
		[Required]
		public string Email { get; set; } = null!;

		[Required]
		[MaxLength(50)]
		public string Username { get; set; } = null!;

		[Required]
		[MinLength(6)]
		public string Password { get; set; } = null!;

		[Required]
		[MinLength(6)]
		public string ConfirmPassword { get; set; } = null!;
	}
}
