using HackerNews.Domain.Common.Models.Users;
using System;

namespace HackerNews.CLI.Requests.Configuration
{
	public interface ILoginRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		TBaseRequestBuilder BaseRequest { get; }
		LoginModel LoginModel { get; set; }
		TBaseRequestBuilder SetWhenBuilt(Func<LoginModel> loginCallback);
	}

	public class LoginRequestConfiguration<TBaseRequestBuilder, TRequest> : ILoginRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		public TBaseRequestBuilder BaseRequest { get; }

		public LoginModel LoginModel { get; set; }

		public LoginRequestConfiguration(TBaseRequestBuilder requestBuilder)
		{
			BaseRequest = requestBuilder;
		}

		public TBaseRequestBuilder SetWhenBuilt(Func<LoginModel> loginCallback)
		{
			BaseRequest.BuildActions.Add(() => LoginModel = loginCallback.Invoke());
			return BaseRequest;
		}
	}
}
