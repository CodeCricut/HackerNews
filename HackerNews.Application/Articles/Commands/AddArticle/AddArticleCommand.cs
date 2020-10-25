using AutoMapper;
using HackerNews.Application.Common.Interfaces;
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

namespace HackerNews.Application.Articles.Commands.AddArticle
{
	public class AddArticleCommand : IRequest<GetArticleModel>
	{
		public AddArticleCommand(PostArticleModel postArticleModel)
		{
			PostArticleModel = postArticleModel;
		}

		public PostArticleModel PostArticleModel { get; }
	}

	public class AddArticleCommandHandler : DatabaseRequestHandler<AddArticleCommand, GetArticleModel>
	{

		public AddArticleCommandHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<GetArticleModel> Handle(AddArticleCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var user = await Mediator.Send(new GetAuthenticatedUserQuery());

				if (user == null) throw new UnauthorizedException("Unable to add article; User is not logged in.");

				Article article = Mapper.Map<Article>(request.PostArticleModel);
				article.PostDate = DateTime.Now;
				article.UserId = user.Id;

				var addedArticle = await UnitOfWork.Articles.AddEntityAsync(article);

				UnitOfWork.SaveChanges();

				return Mapper.Map<GetArticleModel>(addedArticle);
			}
		}
	}
}
