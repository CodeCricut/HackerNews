using HackerNews.Domain;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.Helpers.Cookies.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Default.ArticleServices
{
	public class ArticleApiVoter : IApiVoter<Article>
	{
		protected readonly IJwtService _jwtService;
		protected HttpClient _client;

		public ArticleApiVoter(IHttpClientFactory clientFactory, IOptions<AppSettings> options, IJwtService jwtService)
		{
			_client = clientFactory.CreateClient();
			_client.BaseAddress = new Uri(options.Value.BaseApiAddress);
			_jwtService = jwtService;
		}

		public async Task VoteEntityAsync(int entityId, bool upvote)
		{
			if (_jwtService.ContainsToken())
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtService.GetToken());

			var upvoteString = JsonConvert.SerializeObject(upvote);
			var upvoteJson = new StringContent(upvoteString, UnicodeEncoding.UTF8, "application/json");

			var response = await _client.PostAsync($"articles/vote/{entityId}", upvoteJson);
		}
	}
}
