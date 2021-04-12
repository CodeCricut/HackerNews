using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
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
		private readonly IAdminLevelOperationValidator<Article> _articleOperationValidator;

		public DeleteArticleHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService,
			IAdminLevelOperationValidator<Article> articleOperationValidator) : base(unitOfWork, mediator, mapper, currentUserService)
		{
			_articleOperationValidator = articleOperationValidator;
		}

		public override async Task<bool> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
		{
			var userId = _currentUserService.UserId;
			var user = (await UnitOfWork.Users.GetEntityAsync(userId));

			if (!await UnitOfWork.Articles.EntityExistsAsync(request.Id)) throw new NotFoundException();

			var article = await UnitOfWork.Articles.GetEntityAsync(request.Id);

			// Verify user can delete entity
			var userOwnsEntity = article.UserId != userId;
			var userHasAdminAccess = await _articleOperationValidator.CanDeleteEntityAsync(article, user.AdminLevel);

			if (!(userOwnsEntity || userHasAdminAccess))
				throw new UnauthorizedException();

			// Delete article and save.
			var successful = await UnitOfWork.Articles.DeleteEntityAsync(request.Id);
			UnitOfWork.SaveChanges();

			return successful;
		}
	}
}
