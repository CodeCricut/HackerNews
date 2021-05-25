using HackerNews.CLI.Requests.Configuration;
using HackerNews.Domain.Common.Models.Boards;

namespace HackerNews.CLI.Util
{
	public static class PostEntityOptionsExtensions
	{
		public static PostBoardModel ToPostModel(this IPostBoardOptions options)
		{
			return new PostBoardModel()
			{
				Title = options.Title,
				Description = options.Description
			};
		}
	}
}
