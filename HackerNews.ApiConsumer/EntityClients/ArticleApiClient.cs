using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.Logging;

namespace HackerNews.ApiConsumer.EntityClients
{
	public interface IArticleApiClient : IEntityApiClient<PostArticleModel, GetArticleModel> { }

	internal class ArticleApiClient : EntityApiClient<PostArticleModel, GetArticleModel>, IArticleApiClient
	{
		public ArticleApiClient(IApiClient apiClient, ILogger<ArticleApiClient> logger) : base(apiClient, logger, "articles")
		{
		}
	}
}
