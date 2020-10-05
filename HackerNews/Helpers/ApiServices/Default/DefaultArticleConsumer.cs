using Microsoft.Extensions.Options;
using System.Net.Http;

namespace HackerNews.Helpers
{
	public class DefaultArticleConsumer : ArticleApiConsumer
	{
		public DefaultArticleConsumer(IHttpClientFactory clientFactory, IOptions<AppSettings> options) : base(clientFactory, options)
		{
		}
	}
}
