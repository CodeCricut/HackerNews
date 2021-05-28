using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Images;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Articles.Commands.AddImage
{
	public class AddArticleImageCommand : IRequest<GetArticleModel>
	{
		public AddArticleImageCommand(PostImageModel imageModel, int articleId)
		{
			ImageModel = imageModel;
			ArticleId = articleId;
		}

		public PostImageModel ImageModel { get; }
		public int ArticleId { get; }
	}

	public class AddArticleImageHandler : DatabaseRequestHandler<AddArticleImageCommand, GetArticleModel>
	{
		public AddArticleImageHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetArticleModel> Handle(AddArticleImageCommand request, CancellationToken cancellationToken)
		{
			await VerifyLoggedIn();

			Article article = await GetArticle(request.ArticleId);

			VerifyArticleExists(article);

			VerifyUserCreatedArticle(article);

			await CreateArticleImage(request.ImageModel, article);

			// Return updated article
			return MapArticleToModel(article);
		}

		private async Task VerifyLoggedIn()
		{
			if (!await IsLoggedIn())
				throw new UnauthorizedException();
		}
		
		private Task<bool> IsLoggedIn()
		{
			return UnitOfWork.Users.EntityExistsAsync(_currentUserService.UserId);
		}
		
		private static void VerifyArticleExists(Article article)
		{
			if (article == null) throw new NotFoundException();
		}
		
		private void VerifyUserCreatedArticle(Article article)
		{
			if (article.UserId != _currentUserService.UserId) throw new UnauthorizedException();
		}

		private async Task<Article> GetArticle(int articleId)
		{
			return await UnitOfWork.Articles.GetEntityAsync(articleId);
		}
		
		private async Task CreateArticleImage(PostImageModel imageModel, Article article)
		{
			var imageToAdd = Mapper.Map<Image>(imageModel);
			imageToAdd.ArticleId = article.Id;

			await UnitOfWork.Images.AddEntityAsync(imageToAdd);

			// Add image to article
			article.AssociatedImage = imageToAdd;

			// Save
			UnitOfWork.SaveChanges();
		}

		private GetArticleModel MapArticleToModel(Article article)
		{
			return Mapper.Map<GetArticleModel>(article);
		}
	}
}
