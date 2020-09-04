using AutoMapper;
using HackerNews.Api.Profiles;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
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

		#region Create
		public async Task<GetCommentModel> PostCommentModelAsync(PostCommentModel commentModel)
		{

			Comment comment = await ConvertCommentAsync(commentModel);

			var addedComment = (await _commentRepository.AddCommentAsync(comment));

			await Task.WhenAll(
				_commentRepository.SaveChangesAsync(),
				_articleRepository.SaveChangesAsync());

			return _mapper.Map<GetCommentModel>(addedComment);
		}

		public async Task PostCommentModelsAsync(List<PostCommentModel> commentModels)
		{
			List<Comment> convertedComments = await ConvertCommentsAsync(commentModels);

			await _commentRepository.AddCommentsAsync(convertedComments);

			await Task.WhenAll(
				_commentRepository.SaveChangesAsync(),
				_articleRepository.SaveChangesAsync());
		}
		#endregion

		#region Read
		public async Task<GetCommentModel> GetCommentModelAsync(int id)
		{
			Comment comment = await GetCommentAsync(id);
			TrimComment(comment);

			return _mapper.Map<GetCommentModel>(comment);
		}

		public async Task<List<GetCommentModel>> GetAllCommentModelsAsync()
		{
			var comments = (await _commentRepository.GetCommentsAsync(true)).ToList();

			// in order to avoid altering the actual references (such as children comments), we must deep clone the list
			var cloneComments = comments.ConvertAll(c => new Comment(c));
			TrimComments(cloneComments);

			return _mapper.Map<IEnumerable<GetCommentModel>>(cloneComments).ToList();
		}
		#endregion

		#region Update
		public async Task<GetCommentModel> PutCommentModelAsync(int id, PostCommentModel commentModel)
		{
			var comment = await GetCommentAsync(id);
			UpdateCommentProperties(commentModel, comment);

			await _commentRepository.UpdateCommentAsync(id, comment);
			await _commentRepository.SaveChangesAsync();

			comment = await GetCommentAsync(id);
			return _mapper.Map<GetCommentModel>(comment);
		}

		public async Task VoteCommentAsync(int commentId, bool upvote)
		{
			var comment = await GetCommentAsync(commentId);

			comment.Karma = upvote ? comment.Karma + 1 : comment.Karma - 1;

			await _commentRepository.UpdateCommentAsync(commentId, comment);
			await _commentRepository.SaveChangesAsync();
		}

		public async Task TryAddParentArticleAsync(int articleId, Comment childComment)
		{
			// if no parent article given
			if (articleId < 1) return;
			Article parentArticle = await GetArticleAsync(articleId);

			// add parent to child
			childComment.ParentArticle = parentArticle;
			await _commentRepository.SaveChangesAsync();
		}

		public async Task TryAddParentCommentAsync(int parentId, Comment childComment)
		{
			// if no parent comment given
			if (parentId < 1) return;

			Comment parentComment = await GetCommentAsync(parentId);

			// add the parent reference to the child
			childComment.ParentComment = parentComment;
			await _commentRepository.SaveChangesAsync();
		}
		#endregion

		#region Delete
		public async Task DeleteCommentAsync(int id)
		{
			// verify the comment exists
			await GetCommentAsync(id);

			await _commentRepository.DeleteCommentAsync(id);
			await _commentRepository.SaveChangesAsync();
		}
		#endregion

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

		// Add both the comment and article parents
		private async Task AddParentRelationshipsAsync(PostCommentModel commentModel, Comment comment)
		{
			// add potential parent relationships
			await Task.WhenAll(
				TryAddParentCommentAsync(commentModel.ParentCommentId, comment),
				TryAddParentArticleAsync(commentModel.ParentArticleId, comment));
		}

		/// <summary>
		/// Convert a <see cref="PostCommentModel"/> to a <see cref="Comment"/> and add possible parent relationships.
		/// </summary>
		/// <param name="commentModel"></param>
		/// <returns></returns>
		private async Task<Comment> ConvertCommentAsync(PostCommentModel commentModel)
		{
			try
			{
				var comment = _mapper.Map<Comment>(commentModel);
				await AddParentRelationshipsAsync(commentModel, comment);
				return comment;
			}
			catch (AutoMapperMappingException e)
			{
				throw new InvalidPostException(e.Message);
			}
		}

		private async Task<List<Comment>> ConvertCommentsAsync(List<PostCommentModel> commentModels)
		{
			var commentConversionTasks = new List<Task<Comment>>();
			foreach (var commentModel in commentModels)
			{
				commentConversionTasks.Add(ConvertCommentAsync(commentModel));
			};

			List<Comment> convertedComments = (await Task.WhenAll(commentConversionTasks)).ToList();
			return convertedComments;
		}

		private async Task<Comment> GetCommentAsync(int id)
		{
			var comment = await _commentRepository.GetCommentAsync(id, true);
			if (comment == null) throw new NotFoundException("The comment with the given ID could not be found. Pleasue ensure that the ID is valid.");

			return comment;
		}

		private async Task<Article> GetArticleAsync(int articleId)
		{
			var article = await _articleRepository.GetArticleAsync(articleId);
			if (article == null) throw new NotFoundException();
			return article;
		}
	}
}
