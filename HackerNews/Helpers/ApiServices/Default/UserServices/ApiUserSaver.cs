using HackerNews.Domain.Entities;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.Helpers.Cookies.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Default.UserServices
{
	public class ApiUserSaver : IApiUserSaver<Article>, IApiUserSaver<Comment>
	{
		protected readonly IJwtService _jwtService;
		protected HttpClient _client;

		public ApiUserSaver(IHttpClientFactory clientFactory, IOptions<AppSettings> options, IJwtService jwtService)
		{
			_client = clientFactory.CreateClient();
			_client.BaseAddress = new Uri(options.Value.BaseApiAddress);
			_jwtService = jwtService;
		}

		async Task IApiUserSaver<Article>
			.SaveEntityToUserAsync(int articleId)
		{
			if (_jwtService.ContainsToken())
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtService.GetToken());


			var queryString = new Dictionary<string, string>();
			queryString["articleId"] = articleId.ToString();

			var response = await _client.PostAsync(QueryHelpers.AddQueryString($"users/save-article", queryString), null);
		}

		async Task IApiUserSaver<Comment>
			.SaveEntityToUserAsync(int commentId)
		{
			if (_jwtService.ContainsToken())
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtService.GetToken());


			var queryString = new Dictionary<string, string>();
			queryString["commentId"] = commentId.ToString();

			var response = await _client.PostAsync(QueryHelpers.AddQueryString($"users/save-comment", queryString), null);
		}

	}
}
