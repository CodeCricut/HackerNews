using HackerNews.CLI.Requests;
using HackerNews.CLI.Verbs.GetBoardById;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.Configuration
{
	public interface IVerbosityRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		TBaseRequestBuilder BaseRequest { get; }
		bool Verbose { get; set; }
		TBaseRequestBuilder SetWhenBuilt(Func<bool> verboseCallback);
	}

	public class VerbosityRequestConfiguration<TBaseRequestBuilder, TRequest>
		: IVerbosityRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		public TBaseRequestBuilder BaseRequest { get; private set; }

		public bool Verbose { get; set; }

		public VerbosityRequestConfiguration(TBaseRequestBuilder requestBuilder)
		{
			BaseRequest = requestBuilder;
		}

		public TBaseRequestBuilder SetWhenBuilt(Func<bool> verboseCallback)
		{
			BaseRequest.BuildActions.Add(() => Verbose = verboseCallback());
			return BaseRequest;
		}
	}
}
