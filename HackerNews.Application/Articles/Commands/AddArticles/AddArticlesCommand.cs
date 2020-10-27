using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Articles.Commands.AddArticles
{
	public class AddArticlesCommand : IRequest<IEnumerable<GetArticleModel>>
	{
		public AddArticlesCommand(IEnumerable<PostArticleModel> postArticleModels)
		{
			PostArticleModels = postArticleModels;
		}

		public IEnumerable<PostArticleModel> PostArticleModels { get; }
	}

	public class AddArticlesCommandHandler : DatabaseRequestHandler<AddArticlesCommand, IEnumerable<GetArticleModel>>
	{
		public AddArticlesCommandHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<IEnumerable<GetArticleModel>> Handle(AddArticlesCommand request, CancellationToken cancellationToken)
		{
			var userId = _currentUserService.UserId;
			if (!await UnitOfWork.Users.EntityExistsAsync(userId)) throw new UnauthorizedException();
			var user = await UnitOfWork.Users.GetEntityAsync(userId);

			var articles = Mapper.Map<IEnumerable<Article>>(request.PostArticleModels);
			foreach (var article in articles)
			{
				article.PostDate = DateTime.Now;
				article.UserId = user.Id;
			}

			var addedArticles = await UnitOfWork.Articles.AddEntititesAsync(articles);

			// My first bug caught by a integration test... brings a tear to my eye >_<
			UnitOfWork.SaveChanges(); // missing before

			return Mapper.Map<IEnumerable<GetArticleModel>>(addedArticles);
		}
	}
}
