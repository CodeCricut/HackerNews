using HackerNews.CLI.Requests;
using HackerNews.CLI.Verbs.GetBoardById;
using HackerNews.CLI.Verbs.GetEntity;
using System;
using System.Collections.Generic;
using System.Text;

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
