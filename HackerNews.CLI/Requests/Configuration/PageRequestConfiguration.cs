using HackerNews.CLI.Requests;
using HackerNews.Domain.Common.Models;
using System;

namespace HackerNews.CLI.Options.Configuration
{
	public interface IPageRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		TBaseRequestBuilder BaseRequest { get; }
		PagingParams PagingParams { get; set; }
		TBaseRequestBuilder SetWhenBuilt(Func<PagingParams> pagingParamsCallback);
	}

	public class PageRequestConfiguration<TBaseRequestBuilder, TRequest> : IPageRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		public TBaseRequestBuilder BaseRequest { get; }

		public PagingParams PagingParams { get; set; }

		public PageRequestConfiguration(TBaseRequestBuilder requestBuilder)
		{
			BaseRequest = requestBuilder;

			PagingParams = new PagingParams();
		}


		public TBaseRequestBuilder SetWhenBuilt(Func<PagingParams> pagingParamsCallback)
		{
			BaseRequest.BuildActions.Add(() => PagingParams = pagingParamsCallback());
			return BaseRequest;
		}
	}
}
