using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.EF.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base.ArticleServices
{
	public class VoteArticleService :
		VoteEntityService<Article>
	{
		private readonly IEntityRepository<Article> _entityRepository;
		private readonly IMapper _mapper;

		public VoteArticleService(IEntityRepository<Article> entityRepository, IMapper mapper)
		{
			_entityRepository = entityRepository;
			_mapper = mapper;
		}

		public override async Task VoteEntityAsync(int id, bool upvote, User currentUser)
		{
			if (!await _entityRepository.VerifyExistsAsync(id)) throw new NotFoundException();

			var article = await _entityRepository.GetEntityAsync(id);

			if (upvote)
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
			await _entityRepository.SaveChangesAsync();
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

			article.UsersDisliked.Add(userDislike);
			currentUser.DislikedArticles.Add(userDislike);
		}

		private static void UnlikeEntity(User currentUser, Article article)
		{
			article.Karma--;
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

			article.UsersLiked.Add(userLike);
			currentUser.LikedArticles.Add(userLike);
		}
	}
}
