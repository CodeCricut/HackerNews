using HackerNews.CLI.Requests;
using HackerNews.CLI.Verbs.GetEntity;

namespace HackerNews.CLI.Verbs.Configuration
{
	public static class VerbosityRequestConfigurationExtensions
	{
		public static TBaseRequestBuilder FromOptions<TBaseRequestBuilder, TRequest>(
			this IVerbosityRequestConfiguration<TBaseRequestBuilder, TRequest> verbosityConfiguration,
			IVerbosityOptions options)
			where TBaseRequestBuilder : IRequestBuilder<TRequest>
		{
			verbosityConfiguration.Verbose = options.Verbose;
			return verbosityConfiguration.BaseRequest;
		}
	}
}
