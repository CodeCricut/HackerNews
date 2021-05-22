using HackerNews.CLI.Options;
using HackerNews.CLI.Requests;
using HackerNews.CLI.Verbs.Configuration;
using System;
using System.Collections.Generic;

namespace HackerNews.CLI.Verbs.GetBoardById
{
	public class GetBoardByIdRequestBuilder 
		: IRequestBuilder<GetBoardByIdRequest>
	{
		private readonly GetBoardByIdRequestFactory _requestFactory;

		public List<Action> BuildActions { get; }

		public IdRequestConfiguration<GetBoardByIdRequestBuilder, GetBoardByIdRequest> OverrideId { get; }

		public IBoardInclusionRequestConfiguration<GetBoardByIdRequestBuilder, GetBoardByIdRequest> OverrideInclusion { get; }

		public IVerbosityRequestConfiguration<GetBoardByIdRequestBuilder, GetBoardByIdRequest> OverrideVerbosity { get; }

		public IPrintRequestConfiguration<GetBoardByIdRequestBuilder, GetBoardByIdRequest> OverridePrint { get; }

		public IFileRequestConfiguration<GetBoardByIdRequestBuilder, GetBoardByIdRequest> OverrideFile { get; }

		public GetBoardByIdRequestBuilder(GetBoardByIdRequestFactory requestFactory)
		{
			_requestFactory = requestFactory;

			BuildActions = new List<Action>();

			OverrideId = new IdRequestConfiguration<GetBoardByIdRequestBuilder, GetBoardByIdRequest>(this);
			OverrideInclusion = new BoardInclusionRequestConfiguration<GetBoardByIdRequestBuilder, GetBoardByIdRequest>(this);
			OverrideVerbosity = new VerbosityRequestConfiguration<GetBoardByIdRequestBuilder, GetBoardByIdRequest>(this);
			OverridePrint = new PrintRequestConfiguration<GetBoardByIdRequestBuilder, GetBoardByIdRequest>(this);
			OverrideFile = new FileRequestConfiguration<GetBoardByIdRequestBuilder, GetBoardByIdRequest>(this);
		}

		public GetBoardByIdRequestBuilder Configure(GetBoardByIdOptions options)
		{
			OverrideVerbosity.FromOptions(options);
			OverrideInclusion.FromOptions(options, options);
			OverrideId.FromOptions(options);
			OverridePrint.FromOptions(options);
			OverrideFile.FromOptions(options);
			
			return this;
		}

		public GetBoardByIdRequest Build()
		{
			foreach(var action in BuildActions)
				action.Invoke();
			return _requestFactory.Create(
				OverrideInclusion.BoardInclusionConfiguration,
				OverrideVerbosity.Verbose,
				OverridePrint.Print,
				OverrideFile.FileLocation,
				OverrideId.Id);
		}
	}
}
