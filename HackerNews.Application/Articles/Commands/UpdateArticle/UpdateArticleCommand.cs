using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
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
		private readonly IAdminLevelOperationValidator<Article> _articleOperationValidator;

		public UpdateArticleHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService,
			IAdminLevelOperationValidator<Article> articleOperationValidator) : base(unitOfWork, mediator, mapper, currentUserService)
		{
			_articleOperationValidator = articleOperationValidator;
		}

		public override async Task<GetArticleModel> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
		{
			User currentUser = await GetCurrentUser();
			Article article = await GetArticle(request.ArticleId);

			await VerifyUserCanUpdateArticle(currentUser, article);

			await UpdateArticleWithUpdateModel(article, request.PostArticleModel);
			return MapArticleToModel(article);
		}

		private async Task<User> GetCurrentUser()
		{
			if (! await UserLoggedIn())
				throw new UnauthorizedException();

			return await UnitOfWork.Users.GetEntityAsync(_currentUserService.UserId);
		}

		private async Task<Article> GetArticle(int articleId)
		{
			if (!await ArticleExists(articleId))
				throw new NotFoundException();

			return await UnitOfWork.Articles.GetEntityAsync(articleId);
		}

		private async Task VerifyUserCanUpdateArticle(User user, Article article)
		{
			if (!await UserCanUpdateArticle(user, article))
				throw new UnauthorizedException();
		}

		private async Task UpdateArticleWithUpdateModel(Article article, PostArticleModel updateModel)
		{
			Article sourceArticle = MapPostModelToArticle(updateModel);

			var updatedArticle = ApplyUpdatedSourceProperties(article, sourceArticle);
			
			await UpdateArticleAndSave(updatedArticle);
		}

		private Task<bool> UserLoggedIn()
		{
			return UnitOfWork.Users.EntityExistsAsync(_currentUserService.UserId);
		}

		private async Task<bool> ArticleExists(int articleId)
		{
			return await UnitOfWork.Articles.EntityExistsAsync(articleId);
		}


		private async Task<bool> UserCanUpdateArticle(User currentUser, Article article)
		{
			bool userOwnsArticle = article.UserId != currentUser.Id;
			bool userModeratesArticle = await _articleOperationValidator.CanModifyEntityAsync(article, currentUser.AdminLevel);
			bool userCanUpdateArticle = userOwnsArticle && userModeratesArticle;
			return userCanUpdateArticle;
		}

		private Article MapPostModelToArticle(PostArticleModel updateModel)
		{
			return Mapper.Map<Article>(updateModel);
		}
		

		private Article ApplyUpdatedSourceProperties(Article destination, Article source)
		{
			var articleToReturn = Mapper.Map<Article>(destination);

			// TODO: breaks SRP, should not have to worry about intricacies of updating a article with new information
			// Only update the properties sent via the model.
			// entityBeingUpdated.BoardId = request.PostArticleModel.BoardId;
			articleToReturn.Text = source.Text;
			articleToReturn.Title = source.Title;
			articleToReturn.Type = source.Type;

			return articleToReturn;
		}

		private async Task UpdateArticleAndSave(Article article)
		{
			await UnitOfWork.Articles.UpdateEntityAsync(article.Id, article);
			UnitOfWork.SaveChanges();
		}

		private GetArticleModel MapArticleToModel(Article article)
		{
			return Mapper.Map<GetArticleModel>(article);
		}
	}
}
