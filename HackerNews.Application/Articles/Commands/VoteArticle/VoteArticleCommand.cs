using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Requests;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Entities.JoinEntities;
using HackerNews.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Articles.Commands.VoteArticle
{
	public class VoteArticleCommand : IRequest<GetArticleModel>
	{
		public VoteArticleCommand(int articleId, bool upvote)
		{
			ArticleId = articleId;
			Upvote = upvote;
		}

		public int ArticleId { get; }
		public bool Upvote { get; }
	}

	public class VoteArticleCommandHandler : DatabaseRequestHandler<VoteArticleCommand, GetArticleModel>
	{
		private readonly ICurrentUserService _currentUserService;

		public VoteArticleCommandHandler(IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService) : base(httpContextAccessor)
		{
			_currentUserService = currentUserService;
		}

		public override async Task<GetArticleModel> Handle(VoteArticleCommand request, CancellationToken cancellationToken)
		{
			// TODO: for the love of god, refactor this...
			using (UnitOfWork)
			{
				var userId = _currentUserService.UserId;
				if (!await UnitOfWork.Users.EntityExistsAsync(userId)) throw new UnauthorizedException();
				var currentUser = await UnitOfWork.Users.GetEntityAsync(userId);

				if (!await UnitOfWork.Articles.EntityExistsAsync(request.ArticleId)) throw new NotFoundException();
				var article = await UnitOfWork.Articles.GetEntityAsync(request.ArticleId);

				if (request.Upvote)
				{
					// if the user has liked the entity, unlike it (not dislike)
					if (UserLikedEntity(currentUser, article))
						UnlikeEntity(currentUser, article);
					// if the user hasn't liked the entity, like it
					else
					{
						// if the user dislike the entity, un-dislike it then like it
						if (UserDislikedEntity(currentUser, article))
							UndislikeEntity(currentUser, article);
						LikeEntity(currentUser, article);
					}
				}
				else
				{
					// if the user has dislike the entity, un-dislike it (not like)
					if (UserDislikedEntity(currentUser, article))
						UndislikeEntity(currentUser, article);
					// if the user hasn't disliked the entity, dislike it
					else
					{
						// if the user liked the entity, unlike it then dislike it
						if (UserLikedEntity(currentUser, article))
							UnlikeEntity(currentUser, article);
						DislikeEntity(currentUser, article);
					}
				}

				// Save changes and return model.
				UnitOfWork.SaveChanges();

				return Mapper.Map<GetArticleModel>(article);
			}
		}

		private static bool UserDislikedEntity(User currentUser, Article article)
		{
			return article.UsersDisliked.FirstOrDefault(ud => ud.UserId == currentUser.Id) != null;
		}

		private static bool UserLikedEntity(User currentUser, Article article)
		{
			return article.UsersLiked.FirstOrDefault(ul => ul.UserId == currentUser.Id) != null;
		}

		private static void UndislikeEntity(User currentUser, Article article)
		{
			article.Karma++;
			currentUser.Karma++;
			var joinEntity = article.UsersDisliked.FirstOrDefault(ud => ud.UserId == currentUser.Id);

			article.UsersDisliked.Remove(joinEntity);
			currentUser.DislikedArticles.Remove(joinEntity);
		}

		private static void DislikeEntity(User currentUser, Article article)
		{
			var userDislike = new UserArticleDislikes
			{
				Article = article,
				User = currentUser
			};

			article.Karma--;
			currentUser.Karma--;

			article.UsersDisliked.Add(userDislike);
			currentUser.DislikedArticles.Add(userDislike);
		}

		private static void UnlikeEntity(User currentUser, Article article)
		{
			article.Karma--;
			currentUser.Karma--;

			var joinEntity = article.UsersLiked.FirstOrDefault(ul => ul.UserId == currentUser.Id);

			article.UsersLiked.Remove(joinEntity);
			currentUser.LikedArticles.Remove(joinEntity);
		}

		private static void LikeEntity(User currentUser, Article article)
		{
			var userLike = new UserArticleLikes
			{
				Article = article,
				User = currentUser
			};

			article.Karma++;
			currentUser.Karma++;

			article.UsersLiked.Add(userLike);
			currentUser.LikedArticles.Add(userLike);
		}
	}
}
