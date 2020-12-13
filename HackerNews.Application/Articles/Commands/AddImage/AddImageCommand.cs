using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Images;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Articles.Commands.AddImage
{
	public class AddImageCommand : IRequest<GetArticleModel>
	{
		public AddImageCommand(PostImageModel imageModel, int articleId)
		{
			ImageModel = imageModel;
			ArticleId = articleId;
		}

		public PostImageModel ImageModel { get; }
		public int ArticleId { get; }
	}

	public class AddImageHandler : DatabaseRequestHandler<AddImageCommand, GetArticleModel>
	{
		public AddImageHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetArticleModel> Handle(AddImageCommand request, CancellationToken cancellationToken)
		{
			// Verify article exists
			var article = await UnitOfWork.Articles.GetEntityAsync(request.ArticleId);
			if (article == null) throw new NotFoundException();

			// Create image
			var imageToAdd = Mapper.Map<Image>(request.ImageModel);
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
