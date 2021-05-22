using HackerNews.CLI.Options.Verbs;
using HackerNews.CLI.Requests.Configuration;
using HackerNews.CLI.Verbs.Configuration;
using System;
using System.Collections.Generic;

namespace HackerNews.CLI.Requests.GetArticleById
{
	public class GetArticleByIdRequestBuilder : IRequestBuilder<GetArticleByIdRequest>
	{
		private readonly GetArticleByIdRequestFactory _requestFactory;

		public List<Action> BuildActions { get; }

		public IdRequestConfiguration<GetArticleByIdRequestBuilder, GetArticleByIdRequest> OverrideId { get; }

		public IArticleInclusionRequestConfiguration<GetArticleByIdRequestBuilder, GetArticleByIdRequest> OverrideInclusion { get; }

		public IVerbosityRequestConfiguration<GetArticleByIdRequestBuilder, GetArticleByIdRequest> OverrideVerbosity { get; }

		public IPrintRequestConfiguration<GetArticleByIdRequestBuilder, GetArticleByIdRequest> OverridePrint { get; }

		public IFileRequestConfiguration<GetArticleByIdRequestBuilder, GetArticleByIdRequest> OverrideFile { get; }

		public GetArticleByIdRequestBuilder(GetArticleByIdRequestFactory requestFactory)
		{
			_requestFactory = requestFactory;

			BuildActions = new List<Action>();

			OverrideId = new IdRequestConfiguration<GetArticleByIdRequestBuilder, GetArticleByIdRequest>(this);
			OverrideInclusion = new ArticleInclusionRequestConfiguration<GetArticleByIdRequestBuilder, GetArticleByIdRequest>(this);
			OverrideVerbosity = new VerbosityRequestConfiguration<GetArticleByIdRequestBuilder, GetArticleByIdRequest>(this);
			OverridePrint = new PrintRequestConfiguration<GetArticleByIdRequestBuilder, GetArticleByIdRequest>(this);
			OverrideFile = new FileRequestConfiguration<GetArticleByIdRequestBuilder, GetArticleByIdRequest>(this);
		}

		// TODO; add to IRequestBuilder interface
		public GetArticleByIdRequestBuilder Configure(GetArticleByIdOptions options)
		{
			return
				 OverrideId.FromOptions(options)
				.OverrideInclusion.FromOptions(options)
				.OverrideVerbosity.FromOptions(options)
				.OverridePrint.FromOptions(options)
				.OverrideFile.FromOptions(options);
		}

		public GetArticleByIdRequest Build()
		{
			foreach (var action in BuildActions)
				action();
			return _requestFactory.Create(
				OverrideInclusion.ArticleInclusionConfiguration,
				OverrideVerbosity.Verbose,
				OverridePrint.Print,
				OverrideFile.FileLocation,
				OverrideId.Id);
		}
	}
}
