using HackerNews.CLI.Options;
using HackerNews.CLI.Requests.Configuration;
using HackerNews.CLI.Verbs.Configuration;
using System;
using System.Collections.Generic;

namespace HackerNews.CLI.Requests.PostBoard
{
	public class PostBoardRequestBuilder : IRequestBuilder<PostBoardRequest>
	{
		private readonly PostBoardRequestFactory _requestFactory;

		public List<Action> BuildActions { get; }

		public IVerbosityRequestConfiguration<PostBoardRequestBuilder, PostBoardRequest> OverrideVerbosity { get; }

		public ILoginRequestConfiguration<PostBoardRequestBuilder, PostBoardRequest> OverrideLoginModel { get; }

		public IPrintRequestConfiguration<PostBoardRequestBuilder, PostBoardRequest> OverridePrint { get; }

		public IFileRequestConfiguration<PostBoardRequestBuilder, PostBoardRequest> OverrideFile { get; }

		public IPostBoardRequestConfiguration<PostBoardRequestBuilder, PostBoardRequest> OverridePostBoardModel { get; }


		public PostBoardRequestBuilder(PostBoardRequestFactory requestFactory)
		{
			_requestFactory = requestFactory;

			BuildActions = new List<Action>();

			OverrideVerbosity = new VerbosityRequestConfiguration<PostBoardRequestBuilder, PostBoardRequest>(this);
			OverrideLoginModel = new LoginRequestConfiguration<PostBoardRequestBuilder, PostBoardRequest>(this);
			OverridePrint = new PrintRequestConfiguration<PostBoardRequestBuilder, PostBoardRequest>(this);
			OverrideFile = new FileRequestConfiguration<PostBoardRequestBuilder, PostBoardRequest>(this);
			OverridePostBoardModel = new PostBoardRequestConfiguration<PostBoardRequestBuilder, PostBoardRequest>(this);
		}

		public PostBoardRequestBuilder Configure(PostBoardOptions options)
		{
			OverrideVerbosity.FromOptions(options);
			OverrideLoginModel.FromOptions(options);
			OverridePrint.FromOptions(options);
			OverrideFile.FromOptions(options);
			OverridePostBoardModel.FromOptions(options);

			return this;
		}

		public PostBoardRequest Build()
		{
			foreach (var action in BuildActions)
				action.Invoke();

			return _requestFactory.Create(
				OverrideVerbosity.Verbose,
				OverrideLoginModel.LoginModel,
				OverridePrint.Print,
				OverrideFile.FileLocation,
				OverridePostBoardModel.PostBoardModel);
		}
	}
}
