using HackerNews.Domain.Common.Models;
using Microsoft.Extensions.Logging;
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

	// TODO: use response pattern instead of throwing if http error
	internal class ApiClient : IApiClient
	{
		private readonly HttpClient _httpClient;
		private readonly ILogger<ApiClient> _logger;
		private static AuthenticationHeaderValue DefaultAuthHeader;

		public ApiClient(HttpClient httpClient, ILogger<ApiClient> logger)
		{
			_httpClient = httpClient;
			_logger = logger;

			logger.LogTrace("Created " + this.GetType().Name);
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
				idQuery += $"&id={ids[i]}";
			}

			string url = $"{endpoint}/range?PageNumber={pagingParams.PageNumber}&PageSize={pagingParams.PageSize}{idQuery}";

			_logger.LogDebug($"Making GET request to {_httpClient.BaseAddress}{url}");

			var response = await _httpClient.GetAsync(url);

			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsAsync<PaginatedList<TEntity>>();

			if (content == null) _logger.LogWarning($"GET response {_httpClient.BaseAddress}{endpoint} could not be parsed as {typeof(PaginatedList<TEntity>).GetType().Name}.");

			return content;
		}

		public async Task<TEntity> GetAsync<TEntity>(string endpoint = "") where TEntity : class
		{
			SetAuthorizationHeaderForInstance();

			_logger.LogDebug($"Making GET request to {_httpClient.BaseAddress}{endpoint}");

			var response = await _httpClient.GetAsync(endpoint);

			response.EnsureSuccessStatusCode();

			var entity = await response.Content.ReadAsAsync<TEntity>();

			if (entity == null) _logger.LogWarning($"GET response {_httpClient.BaseAddress}{endpoint} could not be parsed as {typeof(TEntity).GetType().Name}.");

			return entity;
		}

		public async Task<PaginatedList<TEntity>> GetPageAsync<TEntity>(PagingParams pagingParams, string endpoint = "") where TEntity : class
		{
			SetAuthorizationHeaderForInstance();

			string url = $"{endpoint}?PageNumber={pagingParams.PageNumber}&PageSize={pagingParams.PageSize}";

			_logger.LogDebug($"Making GET request to {_httpClient.BaseAddress}{url}");

			var response = await _httpClient.GetAsync(url);

			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsAsync<PaginatedList<TEntity>>();

			if (content == null) _logger.LogWarning($"GET response {_httpClient.BaseAddress}{endpoint} could not be parsed as {typeof(PaginatedList<TEntity>).GetType().Name}.");

			return content;
		}

		public async Task<TResponse> PostAsync<TPost, TResponse>(TPost postModel, string endpoint = "")
			where TPost : class
			where TResponse : class
		{
			SetAuthorizationHeaderForInstance();
			string url = $"{endpoint}";

			_logger.LogDebug($"Making GET request to {_httpClient.BaseAddress}{url}");

			var response = await _httpClient.PostAsync(url, postModel, new JsonMediaTypeFormatter());

			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsAsync<TResponse>();

			if (content == null) _logger.LogWarning($"GET response {_httpClient.BaseAddress}{endpoint} could not be parsed as {typeof(TResponse).GetType().Name}.");

			return content;
		}

		public void SetAuthorizationHeader(AuthenticationHeaderValue value)
		{
			_logger.LogTrace("Setting authorization header for HTTP Client with base address " + _httpClient.BaseAddress);
			DefaultAuthHeader = value;
		}

		private void SetAuthorizationHeaderForInstance()
		{
			_httpClient.DefaultRequestHeaders.Authorization = DefaultAuthHeader;
		}
	}
}
