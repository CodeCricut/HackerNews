using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Requests.Configuration;
using HackerNews.Domain.Common.Models.Boards;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
