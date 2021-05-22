using HackerNews.CLI.Requests;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Options.Configuration
{
	public static class PageRequestConfigurationExtensions
	{
		public static TBaseRequestBuilder FromOptions<TBaseRequestBuilder, TRequest>(
			this IPageRequestConfiguration<TBaseRequestBuilder, TRequest> pageConfig,
			IPageOptions options)
			where TBaseRequestBuilder : IRequestBuilder<TRequest>
		{
			PagingParams pagingParams = new PagingParams();

			if (options.PageNumber > 0) pagingParams.PageNumber = options.PageNumber;
			if (options.PageSize > 0) pagingParams.PageSize = options.PageSize;

			pageConfig.PagingParams = pagingParams;
			return pageConfig.BaseRequest;
		}
	}
}
