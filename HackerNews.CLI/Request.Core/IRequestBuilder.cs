using HackerNews.CLI.Options.Verbs;
using HackerNews.CLI.Request.Core;
using System;
using System.Collections.Generic;

namespace HackerNews.CLI.Requests
{
	public interface IRequestBuilder<TOptions>
		//where TOptions : IRequestOptions
	{
		TOptions Options { get; }

		IRequestBuilder<TOptions> PassOptions(TOptions options);

		IRequestBuilder<TOptions> HandleWith(IRequestHandler<TOptions> handler);

		IRequest<TOptions> Build();
	}
}
