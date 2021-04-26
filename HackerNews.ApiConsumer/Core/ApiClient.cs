using HackerNews.Domain.Common.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HackerNews.ApiConsumer.Core
{
	internal interface IApiClient
	{
		Task<TResponse> PostAsync<TPost, TResponse>(TPost postModel, string endpoint = "") where TPost : class
																							where TResponse : class;
		Task<TEntity> GetAsync<TEntity>(int id, string endpoint = "") where TEntity : class;
		Task<TEntity> GetAsync<TEntity>(string endpoint = "") where TEntity : class;
		Task<PaginatedList<TEntity>> GetPageAsync<TEntity>(PagingParams pagingParams, string endpoint = "") where TEntity : class;
		Task<PaginatedList<TEntity>> GetAsync<TEntity>(List<int> ids, PagingParams pagingParams, string endpoint = "") where TEntity : class;

		void SetAuthorizationHeader(AuthenticationHeaderValue value);
	}

	internal class ApiClient : IApiClient
	{
		private readonly HttpClient _httpClient;

		private static AuthenticationHeaderValue DefaultAuthHeader;

		public ApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<TEntity> GetAsync<TEntity>(int id, string endpoint = "") where TEntity : class
		{
			var uri = $"{endpoint}/{id}";
			return await GetAsync<TEntity>(uri);
		}

		public async Task<PaginatedList<TEntity>> GetAsync<TEntity>(List<int> ids, PagingParams pagingParams, string endpoint = "") where TEntity : class
		{
			SetAuthorizationHeaderForInstance();

			string idQuery = "";

			for (int i = 0; i < ids.Count; i++)
			{
				//if (i == 0)
				//	idQuery += $"id={ids[i]}";
				//else
				idQuery += $"&id={ids[i]}";
			}

			string url = $"{endpoint}/range?PageNumber={pagingParams.PageNumber}&PageSize={pagingParams.PageSize}{idQuery}";
			var response = await _httpClient.GetAsync(url);

			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsAsync<PaginatedList<TEntity>>();

			return content;
		}

		public async Task<TEntity> GetAsync<TEntity>(string endpoint = "") where TEntity : class
		{
			SetAuthorizationHeaderForInstance();

			var response = await _httpClient.GetAsync(endpoint);

			response.EnsureSuccessStatusCode();

			return await response.Content.ReadAsAsync<TEntity>();
		}

		public async Task<PaginatedList<TEntity>> GetPageAsync<TEntity>(PagingParams pagingParams, string endpoint = "") where TEntity : class
		{
			SetAuthorizationHeaderForInstance();

			string url = $"{endpoint}?PageNumber={pagingParams.PageNumber}&PageSize={pagingParams.PageSize}";
			var response = await _httpClient.GetAsync(url);

			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsAsync<PaginatedList<TEntity>>();
			return content;
		}

		public async Task<TResponse> PostAsync<TPost, TResponse>(TPost postModel, string endpoint = "")
			where TPost : class
			where TResponse : class
		{
			SetAuthorizationHeaderForInstance();
			string url = $"{endpoint}";
			var response = await _httpClient.PostAsync(url, postModel, new JsonMediaTypeFormatter());

			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsAsync<TResponse>();
			return content;
		}

		public void SetAuthorizationHeader(AuthenticationHeaderValue value)
		{
			DefaultAuthHeader = value;
		}

		private void SetAuthorizationHeaderForInstance()
		{
			_httpClient.DefaultRequestHeaders.Authorization = DefaultAuthHeader;
		}
	}
}
