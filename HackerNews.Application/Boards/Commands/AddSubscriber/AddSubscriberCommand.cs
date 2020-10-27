using AutoMapper;
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
			if (!await UnitOfWork.Users.EntityExistsAsync(_currentUserService.UserId)) throw new UnauthorizedException();
			var user = await UnitOfWork.Users.GetEntityAsync(_currentUserService.UserId);

			if (!await UnitOfWork.Boards.EntityExistsAsync(request.BoardId)) throw new NotFoundException();
			var board = await UnitOfWork.Boards.GetEntityAsync(request.BoardId);

			// Remove if already subbed.
			var existingSubscriber = board.Subscribers.FirstOrDefault(s => s.UserId == user.Id);
			if (existingSubscriber != null)
			{
				board.Subscribers.Remove(existingSubscriber);
				user.BoardsSubscribed.Remove(existingSubscriber);

				UnitOfWork.SaveChanges();
				return Mapper.Map<GetBoardModel>(board);
			}

			// Add sub.
			var boardUserSubscriber = new BoardUserSubscriber
			{
				Board = board,
				User = user
			};

			board.Subscribers.Add(boardUserSubscriber);
			user.BoardsSubscribed.Add(boardUserSubscriber);

			UnitOfWork.SaveChanges();

			return Mapper.Map<GetBoardModel>(board);
		}
	}
}
