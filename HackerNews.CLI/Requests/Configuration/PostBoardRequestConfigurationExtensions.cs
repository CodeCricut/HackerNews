using HackerNews.Domain.Common.Models.Boards;

namespace HackerNews.CLI.Requests.Configuration
{
	public static class PostBoardRequestConfigurationExtensions
	{
		public static TBaseRequestBuilder FromOptions<TBaseRequestBuilder, TRequest>(
			this IPostBoardRequestConfiguration<TBaseRequestBuilder, TRequest> postBoardConfig,
			IPostBoardOptions options)
			where TBaseRequestBuilder : IRequestBuilder<TRequest>
		{
			postBoardConfig.PostBoardModel = new PostBoardModel()
			{
				Title = options.Title,
				Description = options.Description
			};
			return postBoardConfig.BaseRequest;
		}
	}
}
