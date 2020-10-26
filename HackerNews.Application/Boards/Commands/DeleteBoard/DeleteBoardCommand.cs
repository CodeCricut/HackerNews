using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
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
		private readonly ICurrentUserService _currentUserService;

		public DeleteBoardHandler(IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService) : base(httpContextAccessor)
		{
			_currentUserService = currentUserService;
		}

		public override async Task<bool> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
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
}
