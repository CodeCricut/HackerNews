using HackerNews.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.ApiConsumer.Core
{
	public interface IEntityApiClient<TPost, TResponse>
	{
		Task<TResponse> PostAsync(TPost postModel);

		Task<TResponse> GetByIdAsync(int id);

		Task<PaginatedList<TResponse>> GetPageAsync(PagingParams pagingParams);

		Task<PaginatedList<TResponse>> GetByIdsAsync(List<int> ids, PagingParams pagingParams);
	}

	internal abstract class EntityApiClient<TPost, TResponse> : IEntityApiClient<TPost, TResponse>
		where TPost : class
		where TResponse : class
	{
		protected IApiClient ApiClient { get; }
		protected string EntityEndpoint { get; }

		public EntityApiClient(IApiClient apiClient,
			string entityEndpoint)
		{
			ApiClient = apiClient;
			EntityEndpoint = entityEndpoint;
		}

		public virtual Task<TResponse> PostAsync(TPost postModel)
			=> ApiClient.PostAsync<TPost, TResponse>(postModel, EntityEndpoint);

		public virtual Task<TResponse> GetByIdAsync(int id)
			=> ApiClient.GetAsync<TResponse>(id, EntityEndpoint);

		public virtual Task<PaginatedList<TResponse>> GetPageAsync(PagingParams pagingParams)
			=> ApiClient.GetPageAsync<TResponse>(pagingParams, EntityEndpoint);

		public virtual Task<PaginatedList<TResponse>> GetByIdsAsync(List<int> ids, PagingParams pagingParams)
			=> ApiClient.GetAsync<TResponse>(ids, pagingParams, EntityEndpoint);
	}
}
