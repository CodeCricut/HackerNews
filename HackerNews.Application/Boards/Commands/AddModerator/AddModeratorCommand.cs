﻿using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models.Boards;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Entities.JoinEntities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Boards.Commands.AddModerator
{
	public class AddModeratorCommand : IRequest<GetBoardModel>
	{
		public AddModeratorCommand(int boardId, int moderatorId)
		{
			BoardId = boardId;
			ModeratorId = moderatorId;
		}

		public int BoardId { get; }
		public int ModeratorId { get; }
	}

	public class AddModeratorHandler : DatabaseRequestHandler<AddModeratorCommand, GetBoardModel>
	{
		public AddModeratorHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetBoardModel> Handle(AddModeratorCommand request, CancellationToken cancellationToken)
		{
			if (!await UnitOfWork.Users.EntityExistsAsync(_currentUserService.UserId)) throw new UnauthorizedException();
			var currentUser = await UnitOfWork.Users.GetEntityAsync(_currentUserService.UserId);

			var board = await UnitOfWork.Boards.GetEntityAsync(request.BoardId);
			var newModerator = await UnitOfWork.Users.GetEntityAsync(request.ModeratorId);

			if (board == null || newModerator == null) throw new NotFoundException();

			// If the current user isn't a moderator of the sub or creator...
			if (board.Creator.Id != currentUser.Id && board.Moderators.FirstOrDefault(boardUserModerator => boardUserModerator.UserId == currentUser.Id) == null)
				throw new UnauthorizedException();

			// Remove the moderator if already moderating the board.
			var existingModerator = board.Moderators.FirstOrDefault(bu => bu.UserId == request.ModeratorId);
			if (existingModerator != null)
			{
				board.Moderators.Remove(existingModerator);
				newModerator.BoardsModerating.Remove(existingModerator);

				UnitOfWork.SaveChanges();
				return Mapper.Map<GetBoardModel>(board);
			}

			// Add the moderator.
			var boardUserModerator = new BoardUserModerator
			{
				Board = board,
				User = newModerator
			};

			board.Moderators.Add(boardUserModerator);
			newModerator.BoardsModerating.Add(boardUserModerator);

			UnitOfWork.SaveChanges();

			return Mapper.Map<GetBoardModel>(board);
		}
	}
}
