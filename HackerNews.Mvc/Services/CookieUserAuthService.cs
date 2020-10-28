using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Application.Users.Queries.GetUserFromLoginModel;
using HackerNews.Domain.Exceptions;
using HackerNews.Mvc.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Services
{
	public class CookieUserAuthService : IUserAuthService
	{
		private readonly IJwtGeneratorService _jwtGeneratorService;
		private readonly IMediator _mediator;
		private readonly IJwtSetterService _jwtSetterService;
		private readonly HttpContext _context;

		public CookieUserAuthService(IJwtGeneratorService jwtGeneratorService, 
			IHttpContextAccessor httpContextAccessor, 
			IMediator mediator,
			IJwtSetterService jwtSetterService)
		{
			_jwtGeneratorService = jwtGeneratorService;
			_mediator = mediator;
			_jwtSetterService = jwtSetterService;
			_context = httpContextAccessor.HttpContext;
		}

		public async Task<Jwt> LogIn(LoginModel loginModel)
		{
			if (_jwtSetterService.ContainsToken()) _jwtSetterService.RemoveToken();
			var jwt = await _jwtGeneratorService.GenererateJwtFromLoginModelAsync(loginModel);

			// Set JWT to send with each request (used by the API)
			_jwtSetterService.SetToken(jwt, 60);


			var user = await _mediator.Send(new GetUserFromLoginModelQuery(loginModel));

			//var claims = new List<Claim>
			//{
			//	new Claim(ClaimTypes.Name, user.Username),
			//	new Claim("FirstName", user.FirstName),
			//	new Claim("LastName", user.LastName),
			//};

			//var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

			//var authProperties = new AuthenticationProperties
			//{
			//	AllowRefresh = true,
			//	ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
			//	IsPersistent = true
			//};

			//// Sign in using the default cookie authentication scheme. Used in conjunction with .UseAuthentication() and [Authorize]
			//await _context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
			//	new ClaimsPrincipal(claimsIdentity),
			//	authProperties
			//	);

			return jwt;

		}

		public async Task LogOut()
		{
			if (_jwtSetterService.ContainsToken()) _jwtSetterService.RemoveToken();
			//await _context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		}
	}
}
