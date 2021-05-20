using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Verbs.Post;
using HackerNews.Domain.Common.Models.Boards;

namespace HackerNews.CLI.Verbs.PostBoard
{
	public interface IPostBoardProcessor : IPostVerbProcessor<PostBoardModel, GetBoardModel, PostBoardOptions>
	{

	}

	public class PostBoardProcessor : PostVerbProcessor<PostBoardModel, GetBoardModel, PostBoardOptions>,
		 IPostBoardProcessor
	{
		public PostBoardProcessor(ISignInManager signInManager, IEntityApiClient<PostBoardModel, GetBoardModel> entityApiClient, IEntityLogger<GetBoardModel> entityLogger) : base(signInManager, entityApiClient, entityLogger)
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
