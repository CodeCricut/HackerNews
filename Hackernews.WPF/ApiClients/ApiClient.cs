using HackerNews.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hackernews.WPF.ApiClients
{
	public interface IApiClient
	{
		Task<TEntity> GetAsync<TEntity>(int id, string endpoint = "")  where TEntity : class;
		Task<PaginatedList<TEntity>> GetPageAsync<TEntity>(PagingParams pagingParams, string endpoint = "") where TEntity : class;
	}

	public class ApiClient : IApiClient
	{
		private readonly HttpClient _httpClient;

		public ApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<TEntity> GetAsync<TEntity>(int id, string endpoint = "") where TEntity : class
		{
			var uri = $"{endpoint}/{id}";
			var response = await _httpClient.GetAsync(uri);

			response.EnsureSuccessStatusCode();

			return await response.Content.ReadAsAsync<TEntity>();
		}

		public async Task<PaginatedList<TEntity>> GetPageAsync<TEntity>(PagingParams pagingParams, string endpoint = "") where TEntity : class
		{
			string url = $"{endpoint}?PageNumber={pagingParams.PageNumber}&PageSize={pagingParams.PageSize}";
			var response = await _httpClient.GetAsync(url);

			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsAsync<PaginatedList<TEntity>>();
			return content;
		}
	}
}
