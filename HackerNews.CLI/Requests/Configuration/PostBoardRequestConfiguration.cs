using HackerNews.Domain.Common.Models.Boards;
using System;

namespace HackerNews.CLI.Requests.Configuration
{
	public interface IPostBoardRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		TBaseRequestBuilder BaseRequest { get; }
		PostBoardModel PostBoardModel { get; set; }
		TBaseRequestBuilder SetWhenBuilt(Func<PostBoardModel> boardModelCallback);
	}

	public class PostBoardRequestConfiguration<TBaseRequestBuilder, TRequest> : IPostBoardRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		public TBaseRequestBuilder BaseRequest { get; }

		public PostBoardModel PostBoardModel { get; set; }

		public PostBoardRequestConfiguration(TBaseRequestBuilder requestBuilder)
		{
			BaseRequest = requestBuilder;

			PostBoardModel = new PostBoardModel();
		}

		public TBaseRequestBuilder SetWhenBuilt(Func<PostBoardModel> boardModelCallback)
		{
			BaseRequest.BuildActions.Add(() => PostBoardModel = boardModelCallback());
			return BaseRequest;
		}
	}
}
