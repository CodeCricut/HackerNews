using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Entities.JoinEntities;
using HackerNews.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Users.Commands.SaveArticleToUser
{
	public class SaveArticleToUserCommand : IRequest<GetPrivateUserModel>
	{
		public SaveArticleToUserCommand(int articleId)
		{
			ArticleId = articleId;
		}

		public int ArticleId { get; }
	}

	public class SaveArticleToUserHandler : DatabaseRequestHandler<SaveArticleToUserCommand, GetPrivateUserModel>
	{
		private readonly ICurrentUserService _currentUserService;

		public SaveArticleToUserHandler(IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService) : base(httpContextAccessor)
		{
			_currentUserService = currentUserService;
		}

		public override async Task<GetPrivateUserModel> Handle(SaveArticleToUserCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var userId = _currentUserService.UserId;
				var user = await UnitOfWork.Users.GetEntityAsync(userId);
				if (user == null) throw new UnauthorizedException();

				var article = await UnitOfWork.Articles.GetEntityAsync(request.ArticleId);
				if (article == null) throw new NotFoundException();

				bool alreadySaved = article.UsersSaved.Where(us => us.UserId == user.Id).Count() > 0;

				// Remove if already saved.
				if (alreadySaved)
				{
					var joinEntity = article.UsersSaved.FirstOrDefault(us => us.UserId == user.Id);
					article.UsersSaved.Remove(joinEntity);
					user.SavedArticles.Remove(joinEntity);

					UnitOfWork.SaveChanges();

					return Mapper.Map<GetPrivateUserModel>(user);
				}

				// Add relationship.
				var userArticle = new UserArticle { Article = article, User = user };

				user.SavedArticles.Add(userArticle);
				article.UsersSaved.Add(userArticle);

				UnitOfWork.SaveChanges();

				return Mapper.Map<GetPrivateUserModel>(user);
			}
		}
	}
}
