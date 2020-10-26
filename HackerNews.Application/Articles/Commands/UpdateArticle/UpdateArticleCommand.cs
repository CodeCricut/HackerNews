using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Articles.Commands.UpdateArticle
{
	public class UpdateArticleCommand : IRequest<GetArticleModel>
	{
		public UpdateArticleCommand(int articleId, PostArticleModel postArticleModel)
		{
			ArticleId = articleId;
			PostArticleModel = postArticleModel;
		}

		public int ArticleId { get; }
		public PostArticleModel PostArticleModel { get; }
	}

	public class UpdateArticleHandler : DatabaseRequestHandler<UpdateArticleCommand, GetArticleModel>
	{
		private readonly ICurrentUserService _currentUserService;

		public UpdateArticleHandler(IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService) : base(httpContextAccessor)
		{
			_currentUserService = currentUserService;
		}

		public override async Task<GetArticleModel> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var userId = _currentUserService.UserId;
				var currentUser = await UnitOfWork.Users.GetEntityAsync(userId);
				if (currentUser == null) throw new UnauthorizedException();

				// Cerify entity trying to update exists.
				if (!await UnitOfWork.Articles.EntityExistsAsync(request.ArticleId)) throw new NotFoundException();

				// Verify user owns the entity.
				var article = await UnitOfWork.Articles.GetEntityAsync(request.ArticleId);
				if (article.UserId != currentUser.Id) throw new UnauthorizedException();

				var entityFromModel = Mapper.Map<Article>(request.PostArticleModel);
				var entityBeingUpdated = Mapper.Map<Article>(article);

				// Only update the properties sent via the model.
				// entityBeingUpdated.BoardId = request.PostArticleModel.BoardId;
				entityBeingUpdated.Text = request.PostArticleModel.Text;
				entityBeingUpdated.Title = request.PostArticleModel.Title;
				entityBeingUpdated.Type = entityFromModel.Type;


				// Update and save.
				await UnitOfWork.Articles.UpdateEntityAsync(request.ArticleId, entityBeingUpdated);
				UnitOfWork.SaveChanges();

				return Mapper.Map<GetArticleModel>(entityBeingUpdated);
			}
		}
	}
}
