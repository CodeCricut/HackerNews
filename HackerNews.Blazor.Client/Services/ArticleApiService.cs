using HackerNews.Application.Common.Models.Articles;
using HackerNews.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HackerNews.Blazor.Client.Services
{
	public class ArticleApiService : IArticleApiService
	{
		private readonly HttpClient _httpClient;

		public ArticleApiService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public Task<GetArticleModel> AddArticleAsync(PostArticleModel postArticleModel)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<GetArticleModel>> AddArticlesAsync(IEnumerable<PostArticleModel> postArticleModels)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteArticleAsync(int articleId)
		{
			throw new NotImplementedException();
		}

		public Task<GetArticleModel> GetArticleAsync(int articleId)
		{
			throw new NotImplementedException();
		}

		public async Task<PaginatedList<GetArticleModel>> GetArticlesAsync(PagingParams pagingParams)
		{
			return await JsonSerializer.DeserializeAsync<PaginatedList<GetArticleModel>>(
				await _httpClient.GetStreamAsync($"api/articles"), 
				new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
		}

		public Task<PaginatedList<GetArticleModel>> GetArticlesByIdsAsync(IEnumerable<int> ids, PagingParams pagingParams)
		{
			throw new NotImplementedException();
		}

		public Task<PaginatedList<GetArticleModel>> GetArticlesBySearchAsync(string searchTerm, PagingParams pagingParams)
		{
			throw new NotImplementedException();
		}

		public Task<GetArticleModel> UpdateArticleAsync(int articleId, PostArticleModel updateModel)
		{
			throw new NotImplementedException();
		}

		public Task<GetArticleModel> VoteArticleAsync(int articleId, bool upvote)
		{
			throw new NotImplementedException();
		}
	}
}
