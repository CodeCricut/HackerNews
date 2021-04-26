using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.ApiConsumer.Account
{
	public interface IPrivateUserApiClient
	{
		Task<GetPrivateUserModel> GetAsync();
	}

	internal class PrivateUserApiClient : IPrivateUserApiClient
	{
		private readonly IApiClient _apiClient;

		public PrivateUserApiClient(IApiClient apiClient)
		{
			_apiClient = apiClient;
		}

		public Task<GetPrivateUserModel> GetAsync()
			=> _apiClient.GetAsync<GetPrivateUserModel>("users/me");
	}
}
