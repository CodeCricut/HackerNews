using AutoMapper;
using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Requests;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
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
		public AddArticlesCommandHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<IEnumerable<GetArticleModel>> Handle(AddArticlesCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var user = await Mediator.Send(new GetAuthenticatedUserQuery());
				if (user == null) throw new UnauthorizedException();

				var articles = Mapper.Map<IEnumerable<Article>>(request.PostArticleModels);
				foreach(var article in articles)
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
