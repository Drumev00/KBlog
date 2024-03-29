﻿using Blog.Infrastructure.Data.Services;
using Blog.Infrastructure.DTOs.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
	public class IdentityController : MyController
	{
		private readonly AuthenticationService _authenticationService;
		public IdentityController(AuthenticationService authenticationService) 
		{
			_authenticationService = authenticationService;
		}

		[AllowAnonymous]
		[HttpPost("register")]
		public Task<string> Register(RegisterRequest request)
		{
			var result = _authenticationService.Register(request);

			return result;
		}

		
		[AllowAnonymous]
		[HttpPost("login")]
		public Task<AuthResponseModel> Login(LoginRequest request)
		{
			var result = this._authenticationService.Login(request);

			return result;
		}

		[HttpPost("refresh-token")]
		public Task<AuthResponseModel> RefreshToken(TokenRequest tokenRequest)
		{
			var result = _authenticationService.VerifyAndGenerateTokenAsync(tokenRequest);

			return result;
		}


	}
}
