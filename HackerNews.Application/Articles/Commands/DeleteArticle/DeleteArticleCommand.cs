using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
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
		public DeleteArticleHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<bool> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
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
