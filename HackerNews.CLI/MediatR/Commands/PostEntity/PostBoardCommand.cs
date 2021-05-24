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
	public class PostBoardCommand : IRequest<GetBoardModel>
		// : PostEntityCommand<PostBoardModel, GetBoardModel>
	{
		public PostBoardCommand(IPostBoardOptions options) // : base(postModel)
		{
			Options = options;
		}

		public IPostBoardOptions Options { get; }
	}

	public class PostBoardCommandHandler : PostEntityCommandHandler<PostBoardModel, GetBoardModel>,
		IRequestHandler<PostBoardCommand, GetBoardModel>
	{
		public PostBoardCommandHandler(IEntityApiClient<PostBoardModel, GetBoardModel> entityApiClient) : base(entityApiClient)
		{
		}

		public Task<GetBoardModel> Handle(PostBoardCommand request, CancellationToken cancellationToken)
		{
			// TODO; converter
			PostBoardModel board = new PostBoardModel()
			{
				Title = request.Options.Title,
				Description = request.Options.Description
			};

			return base.PostEntity(board);
		}
	}
}
