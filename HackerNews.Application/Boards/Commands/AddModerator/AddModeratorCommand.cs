using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models.Boards;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Entities.JoinEntities;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		private readonly ICurrentUserService _currentUserService;

		public AddModeratorHandler(IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService) : base(httpContextAccessor)
		{
			_currentUserService = currentUserService;
		}

		public override async Task<GetBoardModel> Handle(AddModeratorCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var currentUser = await UnitOfWork.Users.GetEntityAsync(_currentUserService.UserId);

				if (currentUser == null) throw new UnauthorizedException();

				var board = await UnitOfWork.Boards.GetEntityAsync(request.BoardId);
				var newModerator = await UnitOfWork.Users.GetEntityAsync(request.ModeratorId);

				if (board == null || newModerator == null) throw new NotFoundException();

				// If the current user isn't a moderator of the sub
				if (board.Moderators.FirstOrDefault(boardUserModerator => boardUserModerator.UserId == currentUser.Id) == null)
					throw new UnauthorizedException();

				var boardUserModerator = new BoardUserModerator
				{
					Board = board,
					User = newModerator
				};

				board.Moderators.Add(boardUserModerator);

				UnitOfWork.SaveChanges();

				return Mapper.Map<GetBoardModel>(board);
			}
		}
	}
}
