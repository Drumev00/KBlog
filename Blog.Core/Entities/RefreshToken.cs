namespace Blog.Core.Entities
{
	public class RefreshToken
	{
		public RefreshToken()
		{
			Id = Guid.NewGuid().ToString();
		}

		public string Id { get; set; }

		public string Token { get; set; }

		public string JwtId { get; set; }

		public bool IsRevoked { get; set; }

		public DateTime AddedOn { get; set; }

		public DateTime EndsOn { get; set; }

		public string UserId { get; set; }

		public User User { get; set; }
	}
}
