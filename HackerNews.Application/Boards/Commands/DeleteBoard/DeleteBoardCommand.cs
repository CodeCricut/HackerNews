using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Boards.Commands.DeleteBoard
{
	public class DeleteBoardCommand : IRequest<bool>
	{
		public DeleteBoardCommand(int id)
		{
			Id = id;
		}

		public int Id { get; }
	}

	public class DeleteBoardHandler : DatabaseRequestHandler<DeleteBoardCommand, bool>
	{
		public DeleteBoardHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<bool> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
		{
			if (!await UnitOfWork.Users.EntityExistsAsync(_currentUserService.UserId)) throw new UnauthorizedException();
			var currentUser = await UnitOfWork.Users.GetEntityAsync(_currentUserService.UserId);

			if (!await UnitOfWork.Boards.EntityExistsAsync(request.Id)) throw new NotFoundException();
			var board = await UnitOfWork.Boards.GetEntityAsync(request.Id);

			// Verify user created the board
			if (board.Creator.Id != currentUser.Id) throw new UnauthorizedException();

			// Delete and save.
			var successful = await UnitOfWork.Boards.DeleteEntityAsync(request.Id);
			UnitOfWork.SaveChanges();

			return successful;
		}
	}
}
