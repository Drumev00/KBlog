namespace Blog.Infrastructure.DTOs.Authentication
{
	public record AuthResponseModel
	{
		public AuthResponseModel()
		{
			Errors = new List<string>();
		}


		public string JwtToken { get; set; } = null!;

		public DateTime ExpiresAt { get; set; }

		public string RefreshToken { get; set; }

		public ICollection<string> Errors { get; }
	}
}
