using System.ComponentModel.DataAnnotations;

namespace Blog.Infrastructure.DTOs.Authentication
{
	public record TokenRequest
	{
		[Required]
		public string Token { get; set; } = null!;

		[Required]
		public string RefreshToken { get; set; } = null!;
	}
}
