using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Request.Core
{
	public class Request<TOptions> : IRequest<TOptions>
		where TOptions : IRequestOptions
	{
		private readonly TOptions _options;
		private readonly IEnumerable<IRequestHandler<TOptions>> _requestHandlers;

		public Request(TOptions options,
			IEnumerable<IRequestHandler<TOptions>> requestHandlers)
		{
			_options = options;
			_requestHandlers = requestHandlers;
		}

		public Task ExecuteAsync()
		{
			List<Task> handlerTasks = new List<Task>();
			foreach (var handler in _requestHandlers)
				handlerTasks.Add(handler.HandleAsync(_options));

			return Task.WhenAll(handlerTasks);
		}

		public Task CancelAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
