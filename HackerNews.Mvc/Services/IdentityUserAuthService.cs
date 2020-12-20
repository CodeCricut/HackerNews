using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Mvc.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Services
{
	/// <summary>
	/// Implement <see cref="IUserAuthService"/> using JWT cookies.
	/// </summary>
	public class IdentityUserAuthService : IUserAuthService
	{
		private readonly SignInManager<User> _signInManager;
		private readonly IJwtGeneratorService _jwtGeneratorService;
		private readonly IMediator _mediator;
		private readonly IJwtSetterService _jwtSetterService;
		private readonly IMapper _mapper;
		private readonly HttpContext _context;

		public IdentityUserAuthService(SignInManager<User> signInManager,
			IJwtGeneratorService jwtGeneratorService,
			IHttpContextAccessor httpContextAccessor,
			IMediator mediator,
			IJwtSetterService jwtSetterService,
			IMapper mapper)
		{
			_signInManager = signInManager;
			_jwtGeneratorService = jwtGeneratorService;
			_mediator = mediator;
			_jwtSetterService = jwtSetterService;
			_mapper = mapper;
			_context = httpContextAccessor.HttpContext;
		}

		/// <summary>
		/// Login with the SignInManager
		/// </summary>
		/// <param name="loginModel"></param>
		/// <returns></returns>
		public async Task LogInAsync(LoginModel loginModel)
		{
			var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, isPersistent: true, false);
			if (!result.Succeeded) throw new InvalidPostException("Invalid login credentials.");
		}

		public async Task LogOutAsync()
		{
			//await _context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			//if (_jwtSetterService.ContainsToken()) _jwtSetterService.RemoveToken();
			await _signInManager.SignOutAsync();
		}
	}
}
