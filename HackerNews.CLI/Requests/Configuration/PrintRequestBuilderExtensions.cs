using HackerNews.CLI.Requests;
using HackerNews.CLI.Verbs.GetEntity;

namespace HackerNews.CLI.Verbs.Configuration
{
	public static class PrintRequestBuilderExtensions
	{
		public static TBaseRequestBuilder FromOptions<TBaseRequestBuilder, TRequest>(
			this IPrintRequestConfiguration<TBaseRequestBuilder, TRequest> printCofiguration,
			IPrintOptions options)
			where TBaseRequestBuilder : IRequestBuilder<TRequest>
		{
			printCofiguration.Print = options.Print;
			return printCofiguration.BaseRequest;
		}
	}
}
