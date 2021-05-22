using HackerNews.CLI.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Options.Configuration
{
	public interface IIdListRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		TBaseRequestBuilder BaseRequest { get; }
		IEnumerable<int> Ids { get; set; }
		TBaseRequestBuilder SetWhenBuilt(Func<IEnumerable<int>> idsCallback);
	}

	public class IdListRequestConfiguration<TBaseRequestBuilder, TRequest> : IIdListRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		public TBaseRequestBuilder BaseRequest { get; }

		public IEnumerable<int> Ids { get; set; }

		public IdListRequestConfiguration(TBaseRequestBuilder requestHandler)
		{
			BaseRequest = requestHandler;

			Ids = new int[0];
		}

		public TBaseRequestBuilder SetWhenBuilt(Func<IEnumerable<int>> idsCallback)
		{
			BaseRequest.BuildActions.Add(() => Ids = idsCallback());
			return BaseRequest;
		}
	}
}
