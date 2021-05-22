using System;
using System.Collections.Generic;

namespace HackerNews.CLI.Requests
{
	public interface IRequestBuilder<TRequest>
	{
		List<Action> BuildActions { get; }
		TRequest Build();
	}

}
