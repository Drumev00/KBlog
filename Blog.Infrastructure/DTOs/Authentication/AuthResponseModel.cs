namespace Blog.Infrastructure.DTOs.Authentication
{
	public record AuthResponseModel
	{
		public AuthResponseModel()
		{
			Errors = new List<string>();
		}

		public string JwtToken { get; set; } = null!;

		public DateTime ExpirationTime { get; set; }

		public List<string> Errors { get; set; }
	}
}
