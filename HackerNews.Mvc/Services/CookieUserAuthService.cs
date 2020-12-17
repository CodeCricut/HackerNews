using HackerNews.Application.Common.Interfaces;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Mvc.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
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
		public async Task<Jwt> LogIn(LoginModel loginModel)
		{
			// signInManager.SignInAsync(user, false);

			if (_jwtSetterService.ContainsToken()) _jwtSetterService.RemoveToken();
			var jwt = await _jwtGeneratorService.GenererateJwtFromLoginModelAsync(loginModel);

			// Set JWT to send with each request (used by the API)
			_jwtSetterService.SetToken(jwt, 60);

			return jwt;

		}

		/// <summary>
		/// Remove the JWT cookie if present.
		/// </summary>
		/// <returns></returns>
		public async Task LogOut()
		{
			if (_jwtSetterService.ContainsToken()) _jwtSetterService.RemoveToken();
		}
	}
}
