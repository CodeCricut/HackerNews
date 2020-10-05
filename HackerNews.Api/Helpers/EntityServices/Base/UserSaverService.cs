﻿using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Users;
using HackerNews.EF.Repositories;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	public abstract class UserSaverService : IUserSaverService
	{
		private readonly IEntityRepository<Article> _articleRepo;
		private readonly IEntityRepository<Comment> _commentRepo;
		private readonly IMapper _mapper;

		public UserSaverService(IEntityRepository<Article> articleRepo,
			IEntityRepository<Comment> commentRepo,
			IMapper mapper)
		{
			_articleRepo = articleRepo;
			_commentRepo = commentRepo;
			_mapper = mapper;
		}

		public virtual async Task<GetPrivateUserModel> SaveArticleToUserAsync(User user, int articleId)
		{
			// TODO: if already saved, unsave
			var article = await _articleRepo.GetEntityAsync(articleId);

			if (user == null || article == null) throw new NotFoundException();

			// add relationship
			var userArticle = new UserArticle { Article = article, User = user };
			user.SavedArticles.Add(userArticle);
			article.UsersSaved.Add(userArticle);

			await _articleRepo.SaveChangesAsync();

			return _mapper.Map< GetPrivateUserModel>(user);
		}

		public virtual async Task<GetPrivateUserModel> SaveCommentToUserAsync(User user, int commentId)
		{
			// TODO: if already saved, unsave
			var comment = await _commentRepo.GetEntityAsync(commentId);

			if (user == null || comment == null) throw new NotFoundException();
			// add relationship
			var userComment = new UserComment { Comment = comment, User = user };
			user.SavedComments.Add(userComment);
			comment.UsersSaved.Add(userComment);

			await _commentRepo.SaveChangesAsync();

			return _mapper.Map<GetPrivateUserModel>(user);
		}
	}
}
