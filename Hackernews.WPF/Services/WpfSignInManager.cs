using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Hackernews.WPF.Services
{
	public interface ISignInManager
	{
		Task SignInAsync(LoginModel loginModel);
		Task SignOutAsync();
	}

	public class WpfSignInManager : ISignInManager
	{
		private readonly IJwtPrincipal _jwtPrincipal;
		private readonly IApiClient _apiClient;

		public WpfSignInManager(IJwtPrincipal jwtPrincipal, IApiClient apiClient)
		{
			_jwtPrincipal = jwtPrincipal;
			_apiClient = apiClient;
		}

		public async Task SignInAsync(LoginModel loginModel)
		{
			var jwt = await _apiClient.PostAsync<LoginModel, Jwt>(loginModel, "account/login");
			_apiClient.SetAuthorizationHeader(new AuthenticationHeaderValue("Bearer", jwt.Token));
			//_jwtPrincipal.SetJwt(jwt);
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
