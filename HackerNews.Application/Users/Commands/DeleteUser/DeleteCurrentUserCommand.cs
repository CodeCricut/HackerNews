using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Users.Commands.DeleteUser
{
	public class DeleteCurrentUserCommand : IRequest<bool>
	{
		public DeleteCurrentUserCommand()
		{
		}
	}

	public class DeleteCurrentUserHandler : DatabaseRequestHandler<DeleteCurrentUserCommand, bool>
	{
		private readonly ICurrentUserService _currentUserService;

		public DeleteCurrentUserHandler(IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService) : base(httpContextAccessor)
		{
			_currentUserService = currentUserService;
		}

		public override async Task<bool> Handle(DeleteCurrentUserCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var userId = _currentUserService.UserId;
				if (!await UnitOfWork.Users.EntityExistsAsync(userId)) throw new UnauthorizedException();
				var user = UnitOfWork.Users.GetEntityAsync(userId);

				var successful = await UnitOfWork.Users.DeleteEntityAsync(user.Id);
				UnitOfWork.SaveChanges();

				return successful;
			}
		}
	}
}
