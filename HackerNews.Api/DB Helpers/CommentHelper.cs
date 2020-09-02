using AutoMapper;
using HackerNews.Api.Profiles;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.DB_Helpers
{
	public class CommentHelper : ICommentHelper
	{
		private readonly ICommentRepository _commentRepository;
		private readonly IArticleRepository _articleRepository;
		private readonly IMapper _mapper;

		public CommentHelper(
			ICommentRepository commentRepository, 
			IArticleRepository articleRepository,
			IMapper mapper)
		{
			_commentRepository = commentRepository;
			_articleRepository = articleRepository;
			_mapper = mapper;
		}

		public async Task<List<GetCommentModel>> GetAllCommentModelsAsync()
		{
			var comments = (await _commentRepository.GetCommentsAsync(true)).ToList();

			// in order to avoid altering the actual references (such as children comments), we must deep clone the list
			var cloneComments = comments.ConvertAll(c => new Comment(c));
			TrimComments(cloneComments);

			return _mapper.Map<IEnumerable<GetCommentModel>>(cloneComments).ToList();
		}

		public async Task<GetCommentModel> GetCommentModelAsync(int id)
		{
			var comment = await _commentRepository.GetCommentAsync(id, true);
			TrimComment(comment);

			return _mapper.Map<GetCommentModel>(comment);
		}

		public async Task PostCommentModelAsync(PostCommentModel commentModel)
		{
			Comment comment = _mapper.Map<Comment>(commentModel);

			// add potential parent relationships
			await Task.WhenAll(
				TryAddParentCommentAsync(commentModel.ParentCommentId, comment), 
				TryAddParentArticleAsync(commentModel.ParentArticleId, comment));

			await _commentRepository.AddCommentAsync(comment);

			await Task.WhenAll(
				_commentRepository.SaveChangesAsync(), 
				_articleRepository.SaveChangesAsync());
		}

		public async Task<GetCommentModel> PutCommentModelAsync(int id, PostCommentModel commentModel)
		{
			var comment = await _commentRepository.GetCommentAsync(id, true);
			UpdateCommentProperties(commentModel, comment);

			await _commentRepository.UpdateCommentAsync(id, comment);
			await _commentRepository.SaveChangesAsync();

			comment = await _commentRepository.GetCommentAsync(id, true);
			return _mapper.Map<GetCommentModel>(comment);
		}

		public async Task DeleteCommentAsync(int id)
		{
			await _commentRepository.DeleteCommentAsync(id);
			await _commentRepository.SaveChangesAsync();
		}

		public async Task VoteCommentAsync(int commentId, bool upvote)
		{
			var comment = await _commentRepository.GetCommentAsync(commentId, true);

			comment.Karma = upvote ? comment.Karma + 1 : comment.Karma - 1; 

			await _commentRepository.UpdateCommentAsync(commentId, comment);
			await _commentRepository.SaveChangesAsync();
		}

		public async Task TryAddParentArticleAsync(int articleId, Comment childComment)
		{
			// if no parent article given
			if (articleId < 1) return;

			Article parentArticle = await _articleRepository.GetArticleAsync(articleId);

			// add parent to child
			childComment.ParentArticle = parentArticle;
			await _commentRepository.SaveChangesAsync();
		}

		public async Task TryAddParentCommentAsync(int parentId, Comment childComment)
		{
			// if no parent comment given
			if (parentId < 1) return;

			Comment parentComment = await _commentRepository.GetCommentAsync(parentId, true);

			// add the parent reference to the child
			childComment.ParentComment = parentComment;
			await _commentRepository.SaveChangesAsync();
		}

		private static void TrimComment(Comment comment)
		{
			EntityTrimmer.GetNewTrimmedComment(comment, false, false);
		}

		private static void TrimComments(List<Comment> comments)
		{
			for (int i = 0; i < comments.Count(); i++)
			{
				var comment = comments[i];
				TrimComment(comment);
			}
		}

		private static void UpdateCommentProperties(PostCommentModel commentModel, Comment comment)
		{
			// this is messy, but a quick fix
			comment.AuthorName = commentModel.AuthorName;
			comment.Text = commentModel.Text;
		}
	}
}
