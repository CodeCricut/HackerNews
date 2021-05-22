using HackerNews.CLI.Options.Verbs;
using HackerNews.CLI.Requests.Configuration;
using HackerNews.CLI.Verbs.Configuration;
using System;
using System.Collections.Generic;

namespace HackerNews.CLI.Requests.GetCommentById
{
	public class GetCommentByIdRequestBuilder : IRequestBuilder<GetCommentByIdRequest>
	{
		private readonly GetCommentByIdRequestFactory _requestFactory;

		public List<Action> BuildActions { get; }

		public IdRequestConfiguration<GetCommentByIdRequestBuilder, GetCommentByIdRequest> OverrideId { get; }

		public ICommentInclusionRequestConfiguration<GetCommentByIdRequestBuilder, GetCommentByIdRequest> OverrideInclusion { get; }

		public IVerbosityRequestConfiguration<GetCommentByIdRequestBuilder, GetCommentByIdRequest> OverrideVerbosity { get; }

		public IPrintRequestConfiguration<GetCommentByIdRequestBuilder, GetCommentByIdRequest> OverridePrint { get; }

		public IFileRequestConfiguration<GetCommentByIdRequestBuilder, GetCommentByIdRequest> OverrideFile { get; }

		public GetCommentByIdRequestBuilder(GetCommentByIdRequestFactory requestFactory)
		{
			_requestFactory = requestFactory;

			BuildActions = new List<Action>();

			OverrideId = new IdRequestConfiguration<GetCommentByIdRequestBuilder, GetCommentByIdRequest>(this);
			OverrideInclusion = new CommentInclusionRequestConfiguration<GetCommentByIdRequestBuilder, GetCommentByIdRequest>(this);
			OverrideVerbosity = new VerbosityRequestConfiguration<GetCommentByIdRequestBuilder, GetCommentByIdRequest>(this);
			OverridePrint = new PrintRequestConfiguration<GetCommentByIdRequestBuilder, GetCommentByIdRequest>(this);
			OverrideFile = new FileRequestConfiguration<GetCommentByIdRequestBuilder, GetCommentByIdRequest>(this);
		}

		public GetCommentByIdRequestBuilder Configure(GetCommentByIdOptions options)
		{
			OverrideVerbosity.FromOptions(options);
			OverrideInclusion.FromOptions(options);
			OverrideId.FromOptions(options);
			OverridePrint.FromOptions(options);
			OverrideFile.FromOptions(options);

			return this;
		}


		public GetCommentByIdRequest Build()
		{
			foreach (var action in BuildActions)
				action();

			return _requestFactory.Create(
				OverrideInclusion.CommentInclusionConfiguration,
				OverrideVerbosity.Verbose,
				OverridePrint.Print,
				OverrideFile.FileLocation,
				OverrideId.Id);
		}
	}
}
