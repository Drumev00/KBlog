using Blog.Infrastructure.Data.Services;
using Blog.Infrastructure.DTOs.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
	[AllowAnonymous]
	public class IdentityController : MyController
	{
		private readonly AuthenticationService _authenticationService;
		public IdentityController(AuthenticationService authenticationService) 
		{
			_authenticationService = authenticationService;
		}

		[HttpPost("register")]
		public Task<string> Register(RegisterRequest request)
		{
			var result = _authenticationService.Register(request);

			return result;
		}

		[HttpPost("login")]
		public Task<AuthResponseModel> Login(LoginRequest request)
		{
			var result = this._authenticationService.Login(request);

			return result;
		}

		[HttpPost("refreshToken")]
		public AuthResponseModel RefreshToken(string jwtToken)
		{
			var result = _authenticationService.VerifyToken(jwtToken);

			return result;
		}
	}
}
