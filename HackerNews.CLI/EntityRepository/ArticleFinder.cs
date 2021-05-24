using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.CLI.EntityRepository
{
	public class ArticleFinder : IEntityFinder<GetArticleModel>
	{
		private readonly IEntityApiClient<PostArticleModel, GetArticleModel> _entityApiClient;
		private readonly ILogger<ArticleFinder> _logger;

		public ArticleFinder(IEntityApiClient<PostArticleModel, GetArticleModel> entityApiClient, ILogger<ArticleFinder> logger)
		{
			_entityApiClient = entityApiClient;
			_logger = logger;

			_logger.LogTrace("Created " + this.GetType().Name);
		}

		public async Task<GetArticleModel> GetByIdAsync(int id)
		{
			_logger.LogTrace("Getting article with ID  " + id);

			var article = await _entityApiClient.GetByIdAsync(id);

			if (article == null) _logger.LogWarning("Could not find article with ID " + id);

			return article;
		}

		public async Task<PaginatedList<GetArticleModel>> GetByIdsAsync(IEnumerable<int> ids, PagingParams pagingParams)
		{
			_logger.LogTrace("Getting articles with IDs " + ids.ToDelimitedList(','));

			var articlePage = await _entityApiClient.GetByIdsAsync(ids.ToList(), pagingParams);

			if (articlePage.TotalCount != ids.Count()) _logger.LogWarning($"Expected {ids.Count()} articles, got {articlePage.TotalCount}.");

			return articlePage;
		}

		public Task<PaginatedList<GetArticleModel>> GetPageAsync(PagingParams pagingParams)
		{
			_logger.LogTrace("Getting article page");

			return _entityApiClient.GetPageAsync(pagingParams);
		}
	}
}
