﻿using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.EF.Repositories;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	public class UserSaver : IUserSaver
	{
		private readonly IEntityRepository<Article> _articleRepo;
		private readonly IEntityRepository<Comment> _commentRepo;
		private readonly IEntityRepository<User> _userRepo;

		public UserSaver(IEntityRepository<Article> articleRepo,
			IEntityRepository<Comment> commentRepo,
			IEntityRepository<User> userRepo)
		{
			_articleRepo = articleRepo;
			_commentRepo = commentRepo;
			_userRepo = userRepo;
		}

		public async Task<User> SaveArticleToUserAsync(int userId, int articleId)
		{
			var user = await _userRepo.GetEntityAsync(userId);
			var article = await _articleRepo.GetEntityAsync(articleId);

			if (user == null || article == null) throw new NotFoundException();

			// add relationship
			var userArticle = new UserArticle { Article = article, User = user };
			user.SavedArticles.Add(userArticle);
			article.UsersSaved.Add(userArticle);

			await _articleRepo.SaveChangesAsync();

			return user;
		}

		public async Task<User> SaveCommentToUserAsync(int userId, int commentId)
		{
			var user = await _userRepo.GetEntityAsync(userId);
			var comment = await _commentRepo.GetEntityAsync(commentId);

			if (user == null || comment == null) throw new NotFoundException();

			// add relationship
			var userComment = new UserComment { Comment = comment, User = user };
			user.SavedComments.Add(userComment);
			comment.UsersSaved.Add(userComment);

			await _commentRepo.SaveChangesAsync();

			return user;
		}
	}
}
