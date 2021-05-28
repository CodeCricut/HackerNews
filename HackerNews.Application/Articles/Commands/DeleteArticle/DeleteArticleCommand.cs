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
			var user = await GetUser();

			await VerifyArticleExists(request.Id);
			Article article = await GetArticle(request.Id);

			await VerifyUserCanDelete(user, article);

			bool successful = await DeleteArticle(request.Id);
			return successful;
		}

		private async Task<bool> DeleteArticle(int articleId)
		{
			var successful = await UnitOfWork.Articles.DeleteEntityAsync(articleId);
			UnitOfWork.SaveChanges();
			return successful;
		}

		private async Task VerifyUserCanDelete(User user, Article article)
		{
			var userOwnsEntity = article.UserId != _currentUserService.UserId;
			var userHasAdminAccess = await _articleOperationValidator.CanDeleteEntityAsync(article, user.AdminLevel);

			if (!(userOwnsEntity || userHasAdminAccess))
				throw new UnauthorizedException();
		}

		private async Task<Article> GetArticle(int articleId)
		{
			return await UnitOfWork.Articles.GetEntityAsync(articleId);
		}

		private async Task VerifyArticleExists(int articleId)
		{
			if (!await UnitOfWork.Articles.EntityExistsAsync(articleId)) throw new NotFoundException();
		}

		private Task<User> GetUser()
		{
			var userId = _currentUserService.UserId;
			return UnitOfWork.Users.GetEntityAsync(userId);
		}
	}
}
