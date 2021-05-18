using HackerNews.ApiConsumer.EntityClients;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.Services
{
	public class GetArticleProcessor : GetVerbProcessor<GetArticleModel>
	{
		private readonly IArticleApiClient _articleApiClient;
		private readonly IEntityLogger<GetArticleModel> _articleLogger;

		public GetArticleProcessor(IArticleApiClient articleApiClient,
			IEntityLogger<GetArticleModel> articleLogger)
		{
			_articleApiClient = articleApiClient;
			_articleLogger = articleLogger;
		}

		protected override Task<PaginatedList<GetArticleModel>> GetEntitiesAsync(IEnumerable<int> ids, PagingParams pagingParams)
		{
			return _articleApiClient.GetByIdsAsync(ids.ToList(), pagingParams);
		}

		protected override Task<PaginatedList<GetArticleModel>> GetEntitiesAsync(PagingParams pagingParams)
		{
			return _articleApiClient.GetPageAsync(pagingParams);
		}

		protected override Task<GetArticleModel> GetEntityAsync(int id)
		{
			return _articleApiClient.GetByIdAsync(id);
		}

		protected override void OutputEntity(GetArticleModel entity)
		{
			_articleLogger.LogEntity(entity);
		}

		protected override void OutputEntityPage(PaginatedList<GetArticleModel> entityPage)
		{
			_articleLogger.LogEntityPage(entityPage);
		}
	}
}
