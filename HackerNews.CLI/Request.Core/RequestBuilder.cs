using HackerNews.CLI.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Request.Core
{
	public class RequestBuilder<TOptions> : IRequestBuilder<TOptions>
		//where TOptions : IRequestOptions
	{
		public TOptions Options { get; private set; }

		private List<IRequestHandler<TOptions>> _handlers;

		public RequestBuilder()
		{
			_handlers = new List<IRequestHandler<TOptions>>();
		}
		
		public IRequest<TOptions> Build()
		{
			if (_handlers.Count == 0)
				throw new RequestHandlerMissingException();
			
			return new Request<TOptions>(Options, _handlers);
		}

		public IRequestBuilder<TOptions> PassOptions(TOptions options)
		{
			Options = options;
		
			return this;
		}

		public IRequestBuilder<TOptions> HandleWith(IRequestHandler<TOptions> handler)
		{
			if (handler is null)
				throw new ArgumentNullException(nameof(handler));

			_handlers.Add(handler);

			return this;
		}
	}
}
