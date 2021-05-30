using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Entities.JoinEntities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
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
		private const int UPVOTE_KARMA_DELTA = 1;
		private const int DOWNVOTE_KARMA_DELTA = -1;

		private User _currentUser;
		private User _articleOwner;
		private Article _article;

		public VoteArticleCommandHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetArticleModel> Handle(VoteArticleCommand request, CancellationToken cancellationToken)
		{
			await InitializeFields(request);

			if (request.Upvote)
				HandleUpvoteRequest();
			else
				HandleDownvoteRequest();

			UnitOfWork.SaveChanges();
			return Mapper.Map<GetArticleModel>(_article);
		}

		private async Task InitializeFields(VoteArticleCommand request)
		{
			_currentUser = await GetCurrentUser();
			_article = await GetArticleById(request.ArticleId);
			_articleOwner = await GetUserById(_article.UserId);
		}

		private void HandleUpvoteRequest()
		{
			if (UserUpvotedArticle(_currentUser, _article))
				RemoveUserVoteOnArticle();
			else
				RemoveVoteThenUpvoteArticle();
		}

		private void HandleDownvoteRequest()
		{
			if (UserDownvotedArticle(_currentUser, _article))
				UnDownvoteArticle();
			else
				RemoveVoteThenDownvoteArticle();
		}

		private bool UserUpvotedArticle(User user, Article article)
		{
			return GetUserArticleLike(user, article) != null;
		}

		private bool UserDownvotedArticle(User user, Article article)
		{
			return GetUserArticleDislike(user, article) != null;
		}

		private void RemoveVoteThenUpvoteArticle()
		{
			RemoveUserVoteOnArticle();
			UpvoteArticle();
		}
		
		private void RemoveVoteThenDownvoteArticle()
		{
			RemoveUserVoteOnArticle();
			DownvoteArticle();
		}

		private void RemoveUserVoteOnArticle()
		{
			if (UserUpvotedArticle(_currentUser, _article))
				UnUpvoteEntity();
			if (UserDownvotedArticle(_currentUser, _article))
				UnDownvoteArticle();
		}

		private void UpvoteArticle()
		{
			AddKarmaToUserAndArticle(UPVOTE_KARMA_DELTA, _articleOwner, _article);
			AddLikeToUserAndArticle(_currentUser, _article);
		}

		private void DownvoteArticle()
		{
			AddKarmaToUserAndArticle(DOWNVOTE_KARMA_DELTA, _articleOwner, _article);
			AddDislikeToUserAndArticle(_currentUser, _article);
		}

		private void UnUpvoteEntity()
		{
			AddKarmaToUserAndArticle(DOWNVOTE_KARMA_DELTA, _articleOwner, _article);
			RemoveLikeFromUserAndArticle(_currentUser, _article);
		}

		private void UnDownvoteArticle()
		{
			AddKarmaToUserAndArticle(UPVOTE_KARMA_DELTA, _articleOwner, _article);
			RemoveDislikeFromUserAndArticle(_currentUser, _article);
		}

		private async Task<User> GetUserById(int userId)
		{
			if (!await UnitOfWork.Users.EntityExistsAsync(userId))
				throw new NotFoundException();

			return await UnitOfWork.Users.GetEntityAsync(userId);
		}

		private async Task<Article> GetArticleById(int articleId)
		{
			if (!await UnitOfWork.Articles.EntityExistsAsync(articleId)) throw new NotFoundException();
			var article = await UnitOfWork.Articles.GetEntityAsync(articleId);
			return article;
		}

		private async Task<User> GetCurrentUser()
		{
			var userId = _currentUserService.UserId;
			if (!await UnitOfWork.Users.EntityExistsAsync(userId))
				throw new UnauthorizedException();

			return await GetUserById(userId);
		}

		private UserArticleDislikes GetUserArticleDislike(User user, Article article)
		{
			return article.UsersDisliked.FirstOrDefault(ud => ud.UserId == user.Id);
		}

		private UserArticleLikes GetUserArticleLike(User user, Article article)
		{
			return article.UsersLiked.FirstOrDefault(ud => ud.UserId == user.Id);
		}

		private void AddLikeToUserAndArticle(User user, Article article)
		{
			var like = CreateUserArticleLike(user, article);
			_article.UsersLiked.Add(like);
			_currentUser.LikedArticles.Add(like);
		}

		private void RemoveLikeFromUserAndArticle(User user, Article article)
		{
			var like = article.UsersLiked.FirstOrDefault(ul => ul.UserId == user.Id);

			user.LikedArticles.Remove(like);
			article.UsersLiked.Remove(like);
		}

		private void AddDislikeToUserAndArticle(User user, Article article)
		{
			var dislike = CreateUserArticleDislike(user, article);
			user.DislikedArticles.Add(dislike);
			article.UsersDisliked.Add(dislike);
		}

		private void RemoveDislikeFromUserAndArticle(User user, Article article)
		{
			var dislike = GetUserArticleDislike(user, article);
			article.UsersDisliked.Remove(dislike);
			user.DislikedArticles.Remove(dislike);
		}

		private void AddKarmaToUserAndArticle(int karmaToAdd, User user, Article article)
		{
			article.Karma += karmaToAdd;
			user.Karma += karmaToAdd;
		}

		private UserArticleLikes CreateUserArticleLike(User user, Article article)
		{
			return new UserArticleLikes
			{
				Article = article,
				User = user
			};
		}

		private UserArticleDislikes CreateUserArticleDislike(User user, Article article)
		{
			return new UserArticleDislikes
			{
				User = user,
				Article = article,
			};
		}
	}
}
