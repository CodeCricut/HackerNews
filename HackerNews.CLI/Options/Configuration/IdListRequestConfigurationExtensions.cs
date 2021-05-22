using HackerNews.CLI.Requests;
using HackerNews.CLI.Verbs.GetEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Options.Configuration
{
	public static class IdListRequestConfigurationExtensions
	{
		public static TBaseRequestBuilder FromOptions<TBaseRequestBuilder, TRequest>(
			this IIdListRequestConfiguration<TBaseRequestBuilder, TRequest> idListConfig,
			IIdListOptions options)
			where TBaseRequestBuilder : IRequestBuilder<TRequest>
		{
			idListConfig.Ids = options.Ids;
			return idListConfig.BaseRequest;
		}
	}
}
