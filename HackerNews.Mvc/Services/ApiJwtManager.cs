using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
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
		Jwt ApiJwt { get;set; }
		Task<Jwt> LogInAsync(LoginModel loginModel);
		Task LogOutAsync();
	}

	public class ApiJwtManager : IApiJwtManager
	{
		private readonly IGenericHttpClient _httpClient;

		public ApiJwtManager(IGenericHttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public Jwt ApiJwt { get; set; }

		public async Task< Jwt> LogInAsync(LoginModel loginModel)
		{
			Jwt jwt = await _httpClient.PostRequestAsync<LoginModel, Jwt>("https://localhost:44300/api/account/login", loginModel);
			ApiJwt = jwt;

			return ApiJwt;
		}

		public Task LogOutAsync()
		{
			return Task.Factory.StartNew(() => ApiJwt = null);
		}
	}
}
