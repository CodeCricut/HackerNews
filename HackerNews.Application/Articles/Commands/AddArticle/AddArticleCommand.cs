using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System;
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
		public AddArticleCommandHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetArticleModel> Handle(AddArticleCommand request, CancellationToken cancellationToken)
		{
			if (!await UnitOfWork.Users.EntityExistsAsync(_currentUserService.UserId))
				throw new UnauthorizedException("Unable to add article; User is not logged in.");
			var user = await UnitOfWork.Users.GetEntityAsync(_currentUserService.UserId);

			Article article = Mapper.Map<Article>(request.PostArticleModel);
			article.PostDate = DateTime.Now;
			article.UserId = user.Id;

			var addedArticle = await UnitOfWork.Articles.AddEntityAsync(article);

			UnitOfWork.SaveChanges();

			return Mapper.Map<GetArticleModel>(addedArticle);
		}
	}
}
