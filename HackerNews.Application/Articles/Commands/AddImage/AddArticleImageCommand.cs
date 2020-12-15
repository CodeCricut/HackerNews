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
			// Verify logged in
			if (!await UnitOfWork.Users.EntityExistsAsync(_currentUserService.UserId))
				throw new UnauthorizedException();
			var user = await UnitOfWork.Users.GetEntityAsync(_currentUserService.UserId);

			// Verify article exists
			var article = await UnitOfWork.Articles.GetEntityAsync(request.ArticleId);
			if (article == null) throw new NotFoundException();

			// Verify user created article
			if (article.UserId != _currentUserService.UserId) throw new UnauthorizedException();

			// Create image
			var imageToAdd = Mapper.Map<Image>(request.ImageModel);
			imageToAdd.ArticleId = article.Id;

			await UnitOfWork.Images.AddEntityAsync(imageToAdd);

			// Add image to article
			article.AssociatedImage = imageToAdd;

			// Save
			UnitOfWork.SaveChanges();

			// Return updated article
			return Mapper.Map<GetArticleModel>(article);
		}
	}
}
