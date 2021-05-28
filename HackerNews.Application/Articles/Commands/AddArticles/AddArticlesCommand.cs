using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Articles;
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
			User user = await GetUser();
			IEnumerable<Article> articles = MapToArticles(request.PostArticleModels, user);
			IEnumerable<Article> addedArticles = await AddArticlesToDatabase(articles);
			IEnumerable<GetArticleModel> articleModels = MapToModels(addedArticles);
			return articleModels;
		}

		private async Task<User> GetUser()
		{
			var userId = _currentUserService.UserId;
			if (!await UnitOfWork.Users.EntityExistsAsync(userId)) throw new UnauthorizedException();
			var user = await UnitOfWork.Users.GetEntityAsync(userId);
			return user;
		}

		private IEnumerable<Article> MapToArticles(IEnumerable<PostArticleModel> postModels, User user)
		{
			var articles = Mapper.Map<IEnumerable<Article>>(postModels);
			foreach (var article in articles)
			{
				article.PostDate = DateTime.Now;
				article.UserId = user.Id;
			}

			return articles;
		}

		private async Task<IEnumerable<Article>> AddArticlesToDatabase(IEnumerable<Article> articles)
		{
			var addedArticles = await UnitOfWork.Articles.AddEntititesAsync(articles);

			UnitOfWork.SaveChanges();
			return addedArticles;
		}

		private IEnumerable<GetArticleModel> MapToModels(IEnumerable<Article> addedArticles)
		{
			return Mapper.Map<IEnumerable<GetArticleModel>>(addedArticles);
		}
	}
}
