using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Verbs.Post;
using HackerNews.Domain.Common.Models.Boards;

namespace HackerNews.CLI.Verbs.PostBoard
{
	public interface IPostBoardProcessor : IPostVerbProcessor<PostBoardModel, GetBoardModel, PostBoardVerbOptions>
	{

	}

	public class PostBoardProcessor : PostVerbProcessor<PostBoardModel, GetBoardModel, PostBoardVerbOptions>,
		 IPostBoardProcessor
	{
		public PostBoardProcessor(ISignInManager signInManager, IEntityApiClient<PostBoardModel, GetBoardModel> entityApiClient, IEntityLogger<GetBoardModel> entityLogger) : base(signInManager, entityApiClient, entityLogger)
		{
		}

		public override PostBoardModel ConstructPostModel(PostBoardVerbOptions options)
		{
			return new PostBoardModel()
			{
				Title = options.Title,
				Description = options.Description
			};
		}
	}
}
