using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Requests
{
	public interface IRequestBuilder<TRequest>
	{
		List<Action> BuildActions { get; }
		TRequest Build();
	}

}
