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
		private readonly IApiJwtManager _apiJwtManager;

		public IdentityUserAuthService(SignInManager<User> signInManager, IApiJwtManager apiJwtManager)
		{
			_signInManager = signInManager;
			_apiJwtManager = apiJwtManager;
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

			await _apiJwtManager.LogInAsync(loginModel);
		}

		public async Task LogOutAsync()
		{
			await _signInManager.SignOutAsync();
			await _apiJwtManager.LogOutAsync();
		}
	}
}
