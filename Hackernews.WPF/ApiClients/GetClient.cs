using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hackernews.WPF.ApiClients
{
	public interface IGetClient<TEntity> where TEntity : class
	{
		Task<TEntity> GetEntityAsync(int id, string endpoint = "");
		Task<IEnumerable<TEntity>> GetEntitiesAsync(int id, string endpoint = "");
	}

	public class GetClient<TEntity> : IGetClient<TEntity> where TEntity : class
	{
		private readonly HttpClient _httpClient;

		public GetClient(IHttpClientFactory clientFactory)
		{
			clientFactory.CreateClient();
		}

		public async Task<TEntity> GetEntityAsync(int id, string endpoint = "")
		{
			var response = await _httpClient.GetAsync(endpoint);

			response.EnsureSuccessStatusCode();

			var entity = await response.Content.ReadAsAsync<TEntity>();
			return entity;
		}

		public async Task<IEnumerable<TEntity>> GetEntitiesAsync(int id, string endpoint = "")
		{
			var response = await _httpClient.GetAsync(endpoint);

			response.EnsureSuccessStatusCode();

			var entities = await response.Content.ReadAsAsync<IEnumerable<TEntity>>();
			return entities;
		}
	}
}
