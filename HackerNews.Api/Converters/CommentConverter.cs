using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Converters
{
	public class CommentConverter : EntityConverter<Comment, PostCommentModel, GetCommentModel>
	{
		private readonly EntityRepository<Comment> _commentRepository;
		private readonly EntityRepository<Article> _articleRepository;

		public CommentConverter(IMapper mapper, EntityRepository<Comment> commentRepository, EntityRepository<Article> articleRepository) : base(mapper)
		{
			_commentRepository = commentRepository;
			_articleRepository = articleRepository;
		}

		public override async Task<DestinationT> ConvertEntityAsync<DestinationT>(Comment entity)
		{
			return await Task.Factory.StartNew(() => _mapper.Map<DestinationT>(entity));
		}


		public override async Task<Comment> ConvertEntityModelAsync(PostCommentModel entityModel)
		{
			var comment = _mapper.Map<Comment>(entityModel);

			await TryAddParentArticleAsync(entityModel.ParentArticleId, comment);
			await TryAddParentCommentAsync(entityModel.ParentCommentId, comment);

			return comment;
		}

		private async Task TryAddParentCommentAsync(int parentId, Comment childComment)
		{
			// if no parent comment given
			if (parentId < 1) return;

			Comment parentComment = await _commentRepository.GetEntityAsync(parentId);

			// add the parent reference to the child
			childComment.ParentComment = parentComment;
			await _commentRepository.SaveChangesAsync();
		}


		private async Task TryAddParentArticleAsync(int articleId, Comment childComment)
		{
			// if no parent article given
			if (articleId < 1) return;

			Article parentArticle = await _articleRepository.GetEntityAsync(articleId);

			// add parent to child
			childComment.ParentArticle = parentArticle;
			await _commentRepository.SaveChangesAsync();
		}

	}
}
