using HackerNews.CLI.Options;
using HackerNews.Domain.Common.Models.Users;

namespace HackerNews.CLI.Requests.Configuration
{
	public static class LoginRequestConfigurationExtensions
	{
		public static TBaseRequestBuilder FromOptions<TBaseRequestBuilder, TRequest>(
			this ILoginRequestConfiguration<TBaseRequestBuilder, TRequest> loginConfig,
			ILoginOptions options)
			where TBaseRequestBuilder : IRequestBuilder<TRequest>
		{
			loginConfig.LoginModel = new LoginModel() { UserName = options.Username, Password = options.Password };
			return loginConfig.BaseRequest;
		}

		public static TBaseRequestBuilder FromCredentials<TBaseRequestBuilder, TRequest>(
			this ILoginRequestConfiguration<TBaseRequestBuilder, TRequest> loginConfig,
			string username,
			string password)
			where TBaseRequestBuilder : IRequestBuilder<TRequest>
		{
			loginConfig.LoginModel = new LoginModel() { UserName = username, Password = password };
			return loginConfig.BaseRequest;
		}
	}
}
