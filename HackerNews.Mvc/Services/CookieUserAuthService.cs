using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Users.Queries.GetUserFromLoginModel;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Exceptions;
using HackerNews.Mvc.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Services
{
	/// <summary>
	/// Implement <see cref="IUserAuthService"/> using JWT cookies.
	/// </summary>
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

		/// <summary>
		/// Set the JWT cookie if a valid login.
		/// </summary>
		/// <param name="loginModel"></param>
		/// <returns></returns>
		public async Task LogInAsync(LoginModel loginModel)
		{
			// ,,, taken from GenerateJwtFromLoginModelAsync
			var user = await _mediator.Send(new GetUserFromLoginModelQuery(loginModel));
			if (user == null) throw new NotFoundException();

			// AUTH
			var claims = new List<Claim>
			{
				//new Claim(ClaimTypes.Name, user.Username),
				new Claim("id", user.Id.ToString())
			};

			// An identity described by a collection of claims.
			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

			var authProperties = new AuthenticationProperties
			{
				AllowRefresh = false,
				ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7),
				// TODO: make optional with "Remember Me" box
				IsPersistent = true
			};

			// creates an encrypted cookie and adds it to the current response
			await _context.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				authProperties);
			// AUTH

			/// Identity
			// signInManager.SignInAsync(user, false);
			/// Identity

			/// JWT
			//if (_jwtSetterService.ContainsToken()) _jwtSetterService.RemoveToken();
			//var jwt = await _jwtGeneratorService.GenererateJwtFromLoginModelAsync(loginModel);

			//// Set JWT to send with each request (used by the API)
			//_jwtSetterService.SetToken(jwt, 60);

			// return jwt;
			/// JWT
		}

		/// <summary>
		/// Remove the JWT cookie if present.
		/// </summary>
		/// <returns></returns>
		public async Task LogOutAsync()
		{
			/// Auth
			await _context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			/// Auth
	
			/// JWT
			if (_jwtSetterService.ContainsToken()) _jwtSetterService.RemoveToken();
			/// JWT
		}
	}
}
