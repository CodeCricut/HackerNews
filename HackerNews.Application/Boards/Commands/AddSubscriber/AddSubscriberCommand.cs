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

namespace HackerNews.Application.Boards.Commands.AddSubscriber
{
	public class AddSubscriberCommand : IRequest<GetBoardModel>
	{
		public AddSubscriberCommand(int boardId)
		{
			BoardId = boardId;
		}

		public int BoardId { get; }
	}

	public class AddSubscriberHandler : DatabaseRequestHandler<AddSubscriberCommand, GetBoardModel>
	{
		public AddSubscriberHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetBoardModel> Handle(AddSubscriberCommand request, CancellationToken cancellationToken)
		{
			User currentUser = await GetCurrentUser();

			Board board = await GetBoardById(request.BoardId);

			if (UserIsSubscribedToBoard(currentUser, board))
				RemoveExistingSubscriberFromBoard(currentUser, board);
			else
				AddUserToBoardSubscribers(currentUser, board);

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
			if (!await UnitOfWork.Boards.EntityExistsAsync(boardId)) throw new NotFoundException();
			var board = await UnitOfWork.Boards.GetEntityAsync(boardId);
			return board;
		}

		private bool UserIsSubscribedToBoard(User user, Board board)
		{
			return board.Subscribers.Any(s => s.UserId == user.Id);
		}

		private static void RemoveExistingSubscriberFromBoard(User currentUser, Board board)
		{
			var existingSubscriber = new BoardUserSubscriber()
			{
				Board = board,
				User = currentUser
			};
			RemoveBoardUserSubscriberRelationship(currentUser, board, existingSubscriber);
		}

		private static void RemoveBoardUserSubscriberRelationship(User user, Board board, BoardUserSubscriber existingSubscriber)
		{
			board.Subscribers.Remove(existingSubscriber);
			user.BoardsSubscribed.Remove(existingSubscriber);
		}

		private static void AddUserToBoardSubscribers(User user, Board board)
		{
			var boardUserSubscriber = new BoardUserSubscriber
			{
				Board = board,
				User = user
			};

			board.Subscribers.Add(boardUserSubscriber);
			user.BoardsSubscribed.Add(boardUserSubscriber);
		}

		private GetBoardModel MapBoardToModel(Board board)
		{
			return Mapper.Map<GetBoardModel>(board);
		}
	}
}
