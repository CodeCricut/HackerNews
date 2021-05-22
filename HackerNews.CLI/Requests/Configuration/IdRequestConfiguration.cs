using HackerNews.CLI.Requests;
using System;

namespace HackerNews.CLI.Verbs.Configuration
{
	public interface IIdRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		TBaseRequestBuilder BaseRequest { get; }
		int Id { get; set; }
		TBaseRequestBuilder SetWhenBuilt(Func<int> idCallback);
	}

	public class IdRequestConfiguration<TBaseRequestBuilder, TRequest>
		: IIdRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		public TBaseRequestBuilder BaseRequest { get; private set; }

		public int Id { get; set; }

		public TBaseRequestBuilder SetWhenBuilt(Func<int> idCallback)
		{
			BaseRequest.BuildActions.Add(() => Id = idCallback());
			return BaseRequest;
		}

		public IdRequestConfiguration(TBaseRequestBuilder requestBuilder)
		{
			BaseRequest = requestBuilder;
		}
	}
}
