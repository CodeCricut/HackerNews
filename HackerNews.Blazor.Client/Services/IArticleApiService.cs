using HackerNews.Application.Common.Models.Articles;
using HackerNews.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Blazor.Client.Services
{
	public interface IArticleApiService
	{
		Task<GetArticleModel> AddArticleAsync(PostArticleModel postArticleModel);
		Task<IEnumerable<GetArticleModel>> AddArticlesAsync(IEnumerable<PostArticleModel> postArticleModels);
		Task<bool> DeleteArticleAsync(int articleId);
		Task<GetArticleModel> UpdateArticleAsync(int articleId, PostArticleModel updateModel);
		Task<GetArticleModel> VoteArticleAsync(int articleId, bool upvote);
		Task<GetArticleModel> GetArticleAsync(int articleId);
		Task<PaginatedList<GetArticleModel>> GetArticlesByIdsAsync(IEnumerable<int> ids, PagingParams pagingParams);
		Task<PaginatedList<GetArticleModel>> GetArticlesBySearchAsync(string searchTerm, PagingParams pagingParams);
		Task<PaginatedList<GetArticleModel>> GetArticlesAsync(PagingParams pagingParams);
	}
}
