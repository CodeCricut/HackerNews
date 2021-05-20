using HackerNews.Domain.Common.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
		private readonly ILogger<EntityApiClient<TPost, TResponse>> _logger;

		protected IApiClient ApiClient { get; }
		protected string EntityEndpoint { get; }

		public EntityApiClient(IApiClient apiClient,
			ILogger<EntityApiClient<TPost, TResponse>> logger,
			string entityEndpoint)
		{
			ApiClient = apiClient;
			EntityEndpoint = entityEndpoint;

			_logger = logger;
			_logger.LogTrace("Created " + this.GetType().Name);
		}

		public virtual Task<TResponse> PostAsync(TPost postModel)
		{
			_logger.LogDebug("Sending POST request for model of type " + typeof(TPost).GetType().Name);

			return ApiClient.PostAsync<TPost, TResponse>(postModel, EntityEndpoint);
		}

		public virtual Task<TResponse> GetByIdAsync(int id)
		{
			_logger.LogDebug($"Sending GET request for model of type {typeof(TPost).GetType().Name} with ID={id}");

			return ApiClient.GetAsync<TResponse>(id, EntityEndpoint);
		}

		public virtual Task<PaginatedList<TResponse>> GetPageAsync(PagingParams pagingParams)
		{
			_logger.LogDebug($"Sending GET request for page of models of type {typeof(TPost).GetType().Name}");

			return ApiClient.GetPageAsync<TResponse>(pagingParams, EntityEndpoint);
		}

		public virtual Task<PaginatedList<TResponse>> GetByIdsAsync(List<int> ids, PagingParams pagingParams)
		{
			_logger.LogDebug($"Sending GET request for page of models of type {typeof(TPost).GetType().Name} with specified IDs");

			return ApiClient.GetAsync<TResponse>(ids, pagingParams, EntityEndpoint);
		}
	}
}
