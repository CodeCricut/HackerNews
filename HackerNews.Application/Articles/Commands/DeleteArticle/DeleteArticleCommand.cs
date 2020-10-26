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

namespace HackerNews.Application.Articles.Commands.DeleteArticle
{
	public class DeleteArticleCommand : IRequest<bool>
	{
		public DeleteArticleCommand(int id)
		{
			Id = id;
		}

		public int Id { get; }
	}

	public class DeleteArticleHandler : DatabaseRequestHandler<DeleteArticleCommand, bool>
	{
		private readonly ICurrentUserService _currentUserService;

		public DeleteArticleHandler(IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService) : base(httpContextAccessor)
		{
			_currentUserService = currentUserService;
		}

		public override async Task<bool> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var userId = _currentUserService.UserId;

				if (!await UnitOfWork.Articles.EntityExistsAsync(request.Id)) throw new NotFoundException();

				var article = await UnitOfWork.Articles.GetEntityAsync(request.Id);
				// Verify user owns the entity
				if (article.UserId != userId) throw new UnauthorizedException();

				// Delete article and save.
				var successful = await UnitOfWork.Articles.DeleteEntityAsync(request.Id);
				UnitOfWork.SaveChanges();

				return successful;
			}
		}
	}
}
