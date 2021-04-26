using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models.Articles;

namespace HackerNews.ApiConsumer.EntityClients
{
	public interface IArticleApiClient : IEntityApiClient<PostArticleModel, GetArticleModel> { }

	internal class ArticleApiClient : EntityApiClient<PostArticleModel, GetArticleModel>, IArticleApiClient
	{
		public ArticleApiClient(IApiClient apiClient) : base(apiClient, "articles")
		{
		}
	}
}
