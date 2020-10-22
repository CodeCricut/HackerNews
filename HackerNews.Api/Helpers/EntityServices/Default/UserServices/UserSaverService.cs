using AutoMapper;
using CleanEntityArchitecture.Repository;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.JoinEntities;
using HackerNews.Domain.Models.Users;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base.UserServices
{
	public class UserSaverService : IUserSaverService
	{
		private readonly IMapper _mapper;
		private readonly IReadEntityRepository<Article> _readArticleRepo;
		private readonly IReadEntityRepository<Comment> _readCommentRepo;

		public UserSaverService(
			IMapper mapper,
			IReadEntityRepository<Article> readArticleRepo,
			IReadEntityRepository<Comment> readCommentRepo)
		{
			_mapper = mapper;
			_readArticleRepo = readArticleRepo;
			_readCommentRepo = readCommentRepo;
		}

		public virtual async Task<GetPrivateUserModel> SaveArticleToUserAsync(User user, int articleId)
		{
			var article = await _readArticleRepo.GetEntityAsync(articleId);


			// 

			if (user == null || article == null) throw new NotFoundException();

			

			bool alreadySaved = article.UsersSaved.Where(us => us.UserId == user.Id).Count() > 0;

			// remove if already saved
			if (alreadySaved)
			{
				var joinEntity = article.UsersSaved.FirstOrDefault(us => us.UserId == user.Id);
				article.UsersSaved.Remove(joinEntity);
				user.SavedArticles.Remove(joinEntity);

				await _readArticleRepo.SaveChangesAsync();

				return _mapper.Map<GetPrivateUserModel>(user);
			}

			// add relationship
			var userArticle = new UserArticle { Article = article, User = user };

			user.SavedArticles.Add(userArticle);
			article.UsersSaved.Add(userArticle);

			await _readArticleRepo.SaveChangesAsync();

			var updatedArticle = await _readArticleRepo.GetEntityAsync(articleId);


			return _mapper.Map<GetPrivateUserModel>(user);
		}

		public virtual async Task<GetPrivateUserModel> SaveCommentToUserAsync(User user, int commentId)
		{
			// TODO: this is not working

			var comment = await _readCommentRepo.GetEntityAsync(commentId);

			if (user == null || comment == null) throw new NotFoundException();
			// add relationship


			bool alreadySaved = comment.UsersSaved.Where(us => us.UserId == user.Id).Count() > 0;

			// remove if already saved
			if (alreadySaved)
			{
				var joinEntity = comment.UsersSaved.FirstOrDefault(us => us.UserId == user.Id);
				comment.UsersSaved.Remove(joinEntity);
				user.SavedComments.Remove(joinEntity);

				await _readArticleRepo.SaveChangesAsync();

				return _mapper.Map<GetPrivateUserModel>(user);
			}

			var userComment = new UserComment { Comment = comment, User = user };


			user.SavedComments.Add(userComment);
			comment.UsersSaved.Add(userComment);

			await _readCommentRepo.SaveChangesAsync();

			return _mapper.Map<GetPrivateUserModel>(user);
		}
	}
}
