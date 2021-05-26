using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.EntityRepository;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.DataAccess.EntityFinders
{
	public class EntityFinder<TGetModel, TPostModel> : IEntityFinder<TGetModel>
		where TGetModel : GetModelDto
		where TPostModel : PostModelDto
	{
		private readonly IEntityApiClient<TPostModel, TGetModel> _entityApiClient;
		private readonly ILogger<EntityFinder<TGetModel, TPostModel>> _logger;

		public EntityFinder(IEntityApiClient<TPostModel, TGetModel> entityApiClient, 
			ILogger<EntityFinder<TGetModel, TPostModel>> logger)
		{
			_entityApiClient = entityApiClient;
			_logger = logger;
		}

		public async Task<TGetModel> GetByIdAsync(int id)
		{
			var entity = await _entityApiClient.GetByIdAsync(id);

			if (entity == null) _logger.LogWarning("Could not find entity with ID " + id);

			return entity;
		}

		public async Task<PaginatedList<TGetModel>> GetByIdsAsync(IEnumerable<int> ids, PagingParams pagingParams)
		{
			var entityPage = await _entityApiClient.GetByIdsAsync(ids.ToList(), pagingParams);

			if (entityPage.TotalCount != ids.Count()) _logger.LogWarning($"Expected {ids.Count()} entities, got {entityPage.TotalCount}.");

			return entityPage;
		}

		public Task<PaginatedList<TGetModel>> GetPageAsync(PagingParams pagingParams)
		{
			return _entityApiClient.GetPageAsync(pagingParams);
		}
	}
}
