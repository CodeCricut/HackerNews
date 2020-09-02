using HackerNews.Domain;
using HackerNews.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.DB_Helpers
{
	public interface IArticleHelper
	{
		/// <summary>
		/// Get all articles and map them to <see cref="Domain.Models.GetArticleModel"/>
		/// </summary>
		/// <returns></returns>
		public Task<List<GetArticleModel>> GetAllArticleModels();

		/// <summary>
		/// Get an article and map it to <see cref="Domain.Models.GetArticleModel"/>
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Task<GetArticleModel> GetArticleModel(int id);

		/// <summary>
		/// Map the model to an article and add it to the DB.
		/// </summary>
		/// <param name="articleModel"></param>
		public void PostArticleModel(PostArticleModel articleModel);

		/// <summary>
		/// Update the article with the given <paramref name="id"/> using the properties of the <paramref name="articleModel"/>.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="articleModel"></param>
		/// <returns></returns>
		public Task<GetArticleModel> PutArticleModel(int id, PostArticleModel articleModel);

		/// <summary>
		/// Delete the article with the given <paramref name="id"/>.
		/// </summary>
		/// <param name="id"></param>
		public void DeleteArticle(int id);

		/// <summary>
		/// Vote on the article with the given <paramref name="id"/>.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="upvote">true = upvote, false = downvote</param>
		public void VoteArticle(int id, bool upvote);
	}
}
