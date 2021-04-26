using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HackerNews.ApiConsumer.Core
{
	public interface ISignInManager
	{
		Task SignInAsync(LoginModel loginModel);
		Task SignOutAsync();
	}

	internal class ApiConsumerSignInManager : ISignInManager
	{
		private readonly IApiClient _apiClient;

		public ApiConsumerSignInManager(IApiClient apiClient)
		{
			_apiClient = apiClient;
		}

		public async Task SignInAsync(LoginModel loginModel)
		{
			var jwt = await _apiClient.PostAsync<LoginModel, Jwt>(loginModel, "account/login");
			_apiClient.SetAuthorizationHeader(new AuthenticationHeaderValue("Bearer", jwt.Token));
		}

		public async Task SignOutAsync()
		{
			await Task.Factory.StartNew(() =>
			{
				_apiClient.SetAuthorizationHeader(new AuthenticationHeaderValue("Bearer", string.Empty));
			});
		}
	}
}
