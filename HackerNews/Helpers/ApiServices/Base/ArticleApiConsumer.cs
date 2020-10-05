using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace HackerNews.Helpers
{
	public abstract class ArticleApiConsumer : ApiConsumer<Article, PostArticleModel, GetArticleModel>
	{
		public ArticleApiConsumer(IHttpClientFactory clientFactory, IOptions<AppSettings> options) : base(clientFactory, options)
		{
		}
	}
}
