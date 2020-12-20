using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Mvc.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Services
{
	/// <summary>
	/// For fetching, storing, and removing JWT tokens from the API. Useful when the API has to be used by the app, such as when
	/// making API calls from the frontend which require authentication.
	/// </summary>
	public interface IApiJwtManager
	{
		Task<string> LogInAsync(LoginModel loginModel);
		Task LogOutAsync();
		string GetToken();
	}

	public class ApiJwtManager : IApiJwtManager
	{
		private readonly IGenericHttpClient _httpClient;
		private readonly IApiJwtCookieService _apiJwtCookieService;

		public ApiJwtManager(IGenericHttpClient httpClient, IApiJwtCookieService apiJwtCookieService)
		{
			_httpClient = httpClient;
			_apiJwtCookieService = apiJwtCookieService;
		}

		public string GetToken()
		{
			return _apiJwtCookieService.GetToken();
		}

		public async Task< string> LogInAsync(LoginModel loginModel)
		{
			Jwt jwt = await _httpClient.PostRequestAsync<LoginModel, Jwt>("https://localhost:44300/api/account/login", loginModel);
			
			// TODO: add token expiration time to appsettings.json
			_apiJwtCookieService.SetToken(jwt, 6000000);

			return jwt.Token;
		}

		public Task LogOutAsync()
		{
			return Task.Factory.StartNew(() => _apiJwtCookieService.RemoveToken());
		}
	}
}
