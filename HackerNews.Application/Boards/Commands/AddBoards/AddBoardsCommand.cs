using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Entities.JoinEntities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Boards.Commands.AddBoards
{
	public class AddBoardsCommand : IRequest<IEnumerable<GetBoardModel>>
	{
		public AddBoardsCommand(IEnumerable<PostBoardModel> postBoardModels)
		{
			PostBoardModels = postBoardModels;
		}

		public IEnumerable<PostBoardModel> PostBoardModels { get; }
	}

	public class AddBoardsHandler : DatabaseRequestHandler<AddBoardsCommand, IEnumerable<GetBoardModel>>
	{
		public AddBoardsHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<IEnumerable<GetBoardModel>> Handle(AddBoardsCommand request, CancellationToken cancellationToken)
		{
			var user = await UnitOfWork.Users.GetEntityAsync(_currentUserService.UserId);

			if (user == null) throw new UnauthorizedException();

			var boards = Mapper.Map<IEnumerable<Board>>(request.PostBoardModels);
			foreach (var board in boards)
			{
				board.CreateDate = DateTime.Now;
				board.Creator = user;

				// Add creator to moderators
				var boardUser = new BoardUserModerator
				{
					Board = board,
					User = user
				};
				board.Moderators.Add(boardUser);
			}

			var addedBoards = await UnitOfWork.Boards.AddEntititesAsync(boards);
			UnitOfWork.SaveChanges();

			return Mapper.Map<IEnumerable<GetBoardModel>>(addedBoards);
		}
	}
}
