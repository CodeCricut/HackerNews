using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models.Boards;

namespace HackerNews.CLI.Verbs.Post
{
	public interface IPostBoardProcessor : IPostVerbProcessor<PostBoardModel, GetBoardModel>
	{

	}

	public class PostBoardProcessor : PostVerbProcessor<PostBoardModel, GetBoardModel>,
		 IPostBoardProcessor
	{
		public PostBoardProcessor(ISignInManager signInManager, IEntityApiClient<PostBoardModel, GetBoardModel> entityApiClient, IEntityLogger<GetBoardModel> entityLogger) : base(signInManager, entityApiClient, entityLogger)
		{
		}

		protected override PostBoardModel ConstructPostModel(PostVerbOptions options)
		{
			return new PostBoardModel()
			{
				Title = options.BoardTitle,
				Description = options.BoardDescription
			};
		}
	}
}
