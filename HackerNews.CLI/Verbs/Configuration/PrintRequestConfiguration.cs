using HackerNews.CLI.Requests;
using HackerNews.CLI.Verbs.GetBoardById;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.Configuration
{
	public interface IPrintRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		TBaseRequestBuilder BaseRequest { get; }
		bool Print { get; set; }
		TBaseRequestBuilder SetWhenBuilt(Func<bool> printCallback);
	}

	public class PrintRequestConfiguration<TBaseRequestBuilder, TRequest> : IPrintRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		public bool Print { get; set; }

		public TBaseRequestBuilder BaseRequest { get; }

		public PrintRequestConfiguration(TBaseRequestBuilder requestBuilder)
		{
			BaseRequest = requestBuilder;
		}

		public TBaseRequestBuilder SetWhenBuilt(Func<bool> printCallback)
		{
			BaseRequest.BuildActions.Add(() => Print = printCallback());
			return BaseRequest;
		}
	}

}
