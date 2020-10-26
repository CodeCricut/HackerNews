using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Requests;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
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
		private readonly ICurrentUserService _currentUserService;

		public AddArticlesCommandHandler(IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService) : base(httpContextAccessor)
		{
			_currentUserService = currentUserService;
		}

		public override async Task<IEnumerable<GetArticleModel>> Handle(AddArticlesCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
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

				return Mapper.Map<IEnumerable<GetArticleModel>>(addedArticles);
			}
		}
	}
}
