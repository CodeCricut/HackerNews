using Microsoft.Extensions.Options;
using System.Net.Http;

namespace HackerNews.Helpers
{
	public class DefaultUserConsumer : PublicUserApiConsumer
	{
		public DefaultUserConsumer(IHttpClientFactory clientFactory, IOptions<AppSettings> options) : base(clientFactory, options)
		{
		}
	}
}
