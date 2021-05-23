using HackerNews.CLI.Options.Verbs;
using HackerNews.CLI.Verbs.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Requests.GetEntityById
{
	public abstract class RequestBuilder<TRequest, TRequestOptions> :
		IRequestBuilder<TRequest, TRequestOptions>
		where TRequest : IRequest
		where TRequestOptions : IVerbOptions
	{
		public List<Action> BuildActions { get; }

		public TRequestOptions OverrideOptions { get; private set; }

		public RequestBuilder()
		{
			BuildActions = new List<Action>();
		}

		public TRequest Build()
		{
			RunBuildActions();

			return BuildRequest();
		}

		public IRequestBuilder<TRequest, TRequestOptions> Configure(TRequestOptions options)
		{
			OverrideOptions = options;
			return this;
		}

		private void RunBuildActions()
		{
			foreach (var action in BuildActions)
				action.Invoke();
		}

		protected abstract TRequest BuildRequest();
	}
}
