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
			User currentUser = await GetCurrentUser();

			Board board = await GetBoardById(request.BoardId);

			if (!UserCanUpdateBoard(currentUser, board))
				throw new UnauthorizedException();

			await UpdateBoardWithUpdateModel(board, request.PostBoardModel);

			return Mapper.Map<GetBoardModel>(board);
		}

		private async Task<User> GetCurrentUser()
		{
			var currentUser = await UnitOfWork.Users.GetEntityAsync(_currentUserService.UserId);
			if (currentUser == null) throw new UnauthorizedAccessException();
			return currentUser;
		}

		private async Task<Board> GetBoardById(int boardId)
		{
			if (!await UnitOfWork.Boards.EntityExistsAsync(boardId)) throw new NotFoundException();
			var board = await UnitOfWork.Boards.GetEntityAsync(boardId);
			return board;
		}

		private static bool UserCanUpdateBoard(User currentUser, Board board)
		{
			return board.Creator.Id == currentUser.Id ||
							board.Moderators.Any(m => m.UserId == currentUser.Id);
		}

		private async Task UpdateBoardWithUpdateModel(Board board, PostBoardModel updateModel)
		{
			Board sourceBoard = MapPostModelToBoard(updateModel);

			ApplyUpdatedSourceProperties(board, sourceBoard);

			await UpdateBoardAndSave(board);
		}

		private Board MapPostModelToBoard(PostBoardModel postModel)
		{
			return Mapper.Map<Board>(postModel);
		}

		private static void ApplyUpdatedSourceProperties(Board board, Board sourceBoard)
		{
			board.Description = sourceBoard.Description;
		}

		private async Task UpdateBoardAndSave(Board board)
		{
			await UnitOfWork.Boards.UpdateEntityAsync(board.Id, board);
			UnitOfWork.SaveChanges();
		}
	}
}
