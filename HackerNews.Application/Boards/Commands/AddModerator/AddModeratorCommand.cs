using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Entities;
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
			User currentUser = await GetCurrentUser();

			Board board = await GetBoardById(request.BoardId);
			User boardModerator = await GetUserById(request.ModeratorId);

			// TODO; maybe this should be the responsibility of a dedicated class
			VerifyUserCanAddModerator(currentUser, board);

			if (UserIsModeratingBoard(boardModerator, board))
				RemoveExistingModeratorFromBoard(boardModerator, board);
			else
				AddUserToBoardModerators(boardModerator, board);

			UnitOfWork.SaveChanges();

			return MapBoardToModel(board);
		}

		private async Task<User> GetCurrentUser()
		{
			if (!await UnitOfWork.Users.EntityExistsAsync(_currentUserService.UserId)) throw new UnauthorizedException();
			var currentUser = await UnitOfWork.Users.GetEntityAsync(_currentUserService.UserId);
			return currentUser;
		}

		private async Task<Board> GetBoardById(int boardId)
		{
			var board = await UnitOfWork.Boards.GetEntityAsync(boardId);
			if (board == null) throw new NotFoundException();
			return board;
		}

		private async Task<User> GetUserById(int moderatorId)
		{
			var user = await UnitOfWork.Users.GetEntityAsync(moderatorId);
			if (user == null) throw new NotFoundException();
			return user;
		}

		private void VerifyUserCanAddModerator(User currentUser, Board board)
		{
			if (UserOwnsBoard(currentUser, board) &&
							!UserIsModeratingBoard(currentUser, board))
				throw new UnauthorizedException();
		}

		private static bool UserOwnsBoard(User currentUser, Board board)
		{
			return board.Creator.Id != currentUser.Id;
		}

		private static bool UserIsModeratingBoard(User user, Board board)
		{
			return board.Moderators.Any(bu => bu.UserId == user.Id);
		}

		private void RemoveExistingModeratorFromBoard(User existingModerator, Board board)
		{
			BoardUserModerator boardUserMod = board.Moderators.First(bu => bu.UserId == existingModerator.Id);

			RemoveBoardUserModeratorRelationship(board, existingModerator, boardUserMod);
		}

		private void RemoveBoardUserModeratorRelationship(Board board, User newModerator, BoardUserModerator existingModerator)
		{
			board.Moderators.Remove(existingModerator);
			newModerator.BoardsModerating.Remove(existingModerator);
		}

		private static void AddUserToBoardModerators(User newModerator, Board board)
		{
			var boardUserModerator = new BoardUserModerator
			{
				Board = board,
				User = newModerator
			};

			board.Moderators.Add(boardUserModerator);
			newModerator.BoardsModerating.Add(boardUserModerator);
		}

		private GetBoardModel MapBoardToModel(Board board)
		{
			return Mapper.Map<GetBoardModel>(board);
		}
	}
}
