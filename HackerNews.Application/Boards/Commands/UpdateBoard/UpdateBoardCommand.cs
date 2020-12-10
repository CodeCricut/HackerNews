using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Boards.Commands.UpdateBoard
{
	public class UpdateBoardCommand : IRequest<GetBoardModel>
	{
		public UpdateBoardCommand(int boardId, PostBoardModel postBoardModel)
		{
			BoardId = boardId;
			PostBoardModel = postBoardModel;
		}

		public int BoardId { get; }
		public PostBoardModel PostBoardModel { get; }
	}

	public class UpdateBoardHandler : DatabaseRequestHandler<UpdateBoardCommand, GetBoardModel>
	{
		public UpdateBoardHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetBoardModel> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
		{
			var userId = _currentUserService.UserId;
			var currentUser = await UnitOfWork.Boards.GetEntityAsync(userId);
			if (currentUser == null) throw new UnauthorizedAccessException();

			// Verify entity trying to update exists.
			if (!await UnitOfWork.Boards.EntityExistsAsync(request.BoardId)) throw new NotFoundException();

			// Verify user created board or has moderation privileges.
			var board = await UnitOfWork.Boards.GetEntityAsync(request.BoardId);
			if (board.Creator.Id != currentUser.Id &&
				board.Moderators.FirstOrDefault(m => m.UserId == currentUser.Id) == null)
				throw new UnauthorizedException();

			// Update properties
			var updateModel = request.PostBoardModel;
			board.Description = updateModel.Description;

			// Update and save.
			await UnitOfWork.Boards.UpdateEntityAsync(request.BoardId, board);
			UnitOfWork.SaveChanges();

			return Mapper.Map<GetBoardModel>(board);
		}
	}
}
