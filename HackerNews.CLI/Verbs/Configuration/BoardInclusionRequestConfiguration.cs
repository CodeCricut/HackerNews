using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Requests;
using HackerNews.CLI.Verbs.GetBoardById;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.Configuration
{
	public interface IBoardInclusionRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		TBaseRequestBuilder BaseRequest { get; }
		BoardInclusionConfiguration BoardInclusionConfiguration { get; set; }
		TBaseRequestBuilder SetWhenBuilt(Func<BoardInclusionConfiguration> configCallback);
	}

	public class BoardInclusionRequestConfiguration<TBaseRequestBuilder, TRequest>
		: IBoardInclusionRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		public TBaseRequestBuilder BaseRequest { get; }
		public BoardInclusionConfiguration BoardInclusionConfiguration { get; set; }

		public BoardInclusionRequestConfiguration(TBaseRequestBuilder baseRequest)
		{
			BaseRequest = baseRequest;
		}

		public TBaseRequestBuilder SetWhenBuilt(Func<BoardInclusionConfiguration> configCallback)
		{
			BaseRequest.BuildActions.Add(() => BoardInclusionConfiguration = configCallback());
			return BaseRequest;
		}
	}
}
