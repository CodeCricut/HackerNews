using HackerNews.CLI.Options;
using HackerNews.CLI.Options.Configuration;
using HackerNews.CLI.Verbs.Configuration;
using HackerNews.CLI.Verbs.GetBoardById;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Requests.GetBoards
{
	public class GetBoardsRequestBuilder : IRequestBuilder<GetBoardsRequest>
	{
		private readonly GetBoardsRequestFactory _requestFactory;

		public List<Action> BuildActions { get; }

		public IBoardInclusionRequestConfiguration<GetBoardsRequestBuilder, GetBoardsRequest> OverrideInclusion { get; }

		public IVerbosityRequestConfiguration<GetBoardsRequestBuilder, GetBoardsRequest> OverrideVerbosity { get;  }

		public IPrintRequestConfiguration<GetBoardsRequestBuilder, GetBoardsRequest> OverridePrint { get; }

		public IFileRequestConfiguration<GetBoardsRequestBuilder, GetBoardsRequest> OverrideFileLocation { get; }

		public IIdListRequestConfiguration<GetBoardsRequestBuilder, GetBoardsRequest> OverrideIds { get; }

		public IPageRequestConfiguration<GetBoardsRequestBuilder, GetBoardsRequest> OverridePage { get; }


		public GetBoardsRequestBuilder(GetBoardsRequestFactory getBoardsRequestFactory)
		{
			_requestFactory = getBoardsRequestFactory;
		
			BuildActions = new List<Action>();

			OverrideInclusion = new BoardInclusionRequestConfiguration<GetBoardsRequestBuilder, GetBoardsRequest>(this);
			OverrideVerbosity = new VerbosityRequestConfiguration<GetBoardsRequestBuilder, GetBoardsRequest>(this);
			OverridePrint = new PrintRequestConfiguration<GetBoardsRequestBuilder, GetBoardsRequest>(this);
			OverrideFileLocation = new FileRequestConfiguration<GetBoardsRequestBuilder, GetBoardsRequest>(this);
			OverrideIds = new IdListRequestConfiguration<GetBoardsRequestBuilder, GetBoardsRequest>(this);
			OverridePage = new PageRequestConfiguration<GetBoardsRequestBuilder, GetBoardsRequest>(this);
		}

		public GetBoardsRequestBuilder Configure(GetBoardsOptions options)
		{
			return OverrideInclusion.FromOptions(options, options)
				.OverrideVerbosity.FromOptions(options)
				.OverridePrint.FromOptions(options)
				.OverrideFileLocation.FromOptions(options)
				.OverrideIds.FromOptions(options)
				.OverridePage.FromOptions(options);
		}

		public GetBoardsRequest Build()
		{
			foreach (var action in BuildActions)
				action.Invoke();

			return _requestFactory.Create(
				OverrideInclusion.BoardInclusionConfiguration,
				OverrideVerbosity.Verbose,
				OverridePrint.Print,
				OverrideFileLocation.FileLocation,
				OverrideIds.Ids,
				OverridePage.PagingParams);
		}
	}
}
