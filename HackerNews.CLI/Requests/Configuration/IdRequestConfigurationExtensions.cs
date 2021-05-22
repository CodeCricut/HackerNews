using HackerNews.CLI.Requests;
using HackerNews.CLI.Verbs.GetEntity;

namespace HackerNews.CLI.Verbs.Configuration
{
	public static class IdRequestConfigurationExtensions
	{
		public static TBaseRequestBuilder FromOptions<TBaseRequestBuilder, TRequest>(
			this IIdRequestConfiguration<TBaseRequestBuilder, TRequest> idConfiguration,
			IIdOptions options)
			where TBaseRequestBuilder : IRequestBuilder<TRequest>
		{
			idConfiguration.Id = options.Id;
			return idConfiguration.BaseRequest;
		}
	}
}
