using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Configuration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Verbs.Post;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Logging;

namespace HackerNews.CLI.Verbs.PostBoard
{
	public interface IPostBoardProcessor : IPostVerbProcessor<PostBoardModel, GetBoardModel, PostBoardOptions>
	{

	}

	public class PostBoardProcessor : PostVerbProcessor<PostBoardModel, GetBoardModel, PostBoardOptions>,
		 IPostBoardProcessor
	{
		public PostBoardProcessor(ISignInManager signInManager, IEntityApiClient<PostBoardModel, GetBoardModel> entityApiClient, IEntityLogger<GetBoardModel> entityLogger, ILogger<PostVerbProcessor<PostBoardModel, GetBoardModel, PostBoardOptions>> logger, IVerbositySetter verbositySetter) : base(signInManager, entityApiClient, entityLogger, logger, verbositySetter)
		{
		}

		public override PostBoardModel ConstructPostModel(PostBoardOptions options)
		{
			return new PostBoardModel()
			{
				Title = options.Title,
				Description = options.Description
			};
		}
	}
}
