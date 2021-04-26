using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.ApiConsumer.Account
{
	public interface IRegistrationApiClient
	{
		Task<Jwt> RegisterAsync(RegisterUserModel registerModel);
	}

	internal class RegistrationApiClient : IRegistrationApiClient
	{
		private readonly IApiClient _apiClient;

		public RegistrationApiClient(IApiClient apiClient)
		{
			_apiClient = apiClient;
		}

		public Task<Jwt> RegisterAsync(RegisterUserModel registerModel)
			=> _apiClient.PostAsync<RegisterUserModel, Jwt>(registerModel, "account/register");
	}
}
