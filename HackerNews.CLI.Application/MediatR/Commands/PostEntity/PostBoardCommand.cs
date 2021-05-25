using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models.Boards;

namespace HackerNews.CLI.MediatR.Commands.PostEntity
{
	public class PostBoardCommand : PostEntityCommand<PostBoardModel, GetBoardModel>
	{
		public PostBoardCommand(PostBoardModel postModel) : base(postModel)
		{
		}
	}

	public class PostBoardCommandHandler :
		PostEntityCommandHandler<PostBoardCommand, PostBoardModel, GetBoardModel>
	{
		public PostBoardCommandHandler(IEntityApiClient<PostBoardModel, GetBoardModel> entityApiClient) : base(entityApiClient)
		{
		}
	}
}
