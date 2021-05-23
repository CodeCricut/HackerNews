using HackerNews.CLI.Options.Verbs;
using System;
using System.Collections.Generic;

namespace HackerNews.CLI.Requests
{
	public interface IRequestBuilder<TRequest, TRequestOptions>
		where TRequest : IRequest
		where TRequestOptions : IVerbOptions
	{
		List<Action> BuildActions { get; }
		TRequestOptions OverrideOptions { get; }
		IRequestBuilder<TRequest, TRequestOptions> Configure(TRequestOptions options);
		TRequest Build();
	}

}
