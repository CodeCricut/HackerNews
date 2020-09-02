using HackerNews.Domain;
using HackerNews.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.DB_Helpers
{
	public interface ICommentHelper
	{
		/// <summary>
		/// Get all comments from the DB and map them to <see cref="Domain.Models.GetCommentModel"/>s.
		/// </summary>
		/// <returns></returns>
		public Task<List<GetCommentModel>> GetAllCommentModels();

		/// <summary>
		/// Get a comment and map it to <see cref="Domain.Models.GetCommentModel"/>.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Task<GetCommentModel> GetCommentModel(int id);

		/// <summary>
		/// Map the <paramref name="commentModel"/> to a comment and add it to the DB.
		/// </summary>
		/// <param name="commentModel"></param>
		/// <returns></returns>
		public Task PostCommentModel(PostCommentModel commentModel);

		/// <summary>
		/// Update the comment with the given <paramref name="id"/> using the properties of the <paramref name="commentModel"/>.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="commentModel"></param>
		/// <returns></returns>
		public Task<GetCommentModel> PutCommentModel(int id, PostCommentModel commentModel);

		/// <summary>
		/// Delete a comment with the given <paramref name="id"/>.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Task DeleteComment(int id);

		/// <summary>
		/// Vote on a comment with the given <paramref name="id"/>.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="upvote">true = upvote, false = downvote</param>
		/// <returns></returns>
		public Task VoteComment(int id, bool upvote);

		/// <summary>
		/// If there is a valid <paramref name="articleId"/>, add a parent-child relationship to the article-comment.
		/// </summary>
		/// <param name="articleId"></param>
		/// <param name="childComment"></param>
		/// <returns></returns>
		Task TryAddParentArticle(int articleId, Comment childComment);

		/// <summary>
		/// If there is a valid <paramref name="parentId"/> corresponding to a <see cref="Comment.Id"/>, add a parent-child 
		/// relationship between the comments.
		/// </summary>
		/// <param name="parentId"></param>
		/// <param name="childComment"></param>
		/// <returns></returns>
		Task TryAddParentComment(int parentId, Comment childComment);

	}
}
