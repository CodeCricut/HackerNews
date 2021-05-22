using HackerNews.CLI.Requests;
using HackerNews.CLI.Verbs.GetBoardById;
using HackerNews.CLI.Verbs.GetEntity;
using System;
using System.Collections.Generic;
using System.Text;

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
