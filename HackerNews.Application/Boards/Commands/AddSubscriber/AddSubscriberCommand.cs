using HackerNews.Application.Common.Models.Boards;
using HackerNews.Application.Common.Requests;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Domain.Entities.JoinEntities;
using HackerNews.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Http;
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
		public AddSubscriberHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<GetBoardModel> Handle(AddSubscriberCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var user = await Mediator.Send(new GetAuthenticatedUserQuery());

				if (user == null) throw new UnauthorizedException();

				var board = await UnitOfWork.Boards.GetEntityAsync(request.BoardId);
				if (board == null) throw new NotFoundException();

				var boardUserSubscriber = new BoardUserSubscriber
				{
					Board = board,
					UserId = user.Id
				};

				board.Subscribers.Add(boardUserSubscriber);

				UnitOfWork.SaveChanges();

				return Mapper.Map<GetBoardModel>(board);
			}
		}
	}
}
