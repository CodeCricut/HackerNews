using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
