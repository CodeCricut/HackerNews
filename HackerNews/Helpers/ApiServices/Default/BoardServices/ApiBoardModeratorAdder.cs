using HackerNews.Domain.Models.Board;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.Helpers.Cookies.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Default.BoardServices
{
	public class ApiBoardModeratorAdder : IApiBoardModeratorAdder
	{
		private readonly IJwtService _jwtService;
		private HttpClient _client;

		public ApiBoardModeratorAdder(
			IHttpClientFactory clientFactory,
			IOptions<AppSettings> settings,
			IJwtService jwtService
			)
		{
			_client = clientFactory.CreateClient();
			_client.BaseAddress = new Uri(settings.Value.BaseApiAddress);
			_jwtService = jwtService;
		}

		public async Task<GetBoardModel> AddModerator(int boardId, int moderatorId)
		{
			// try attatch JWT token if present
			if (_jwtService.ContainsToken())
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtService.GetToken());


			var query = new Dictionary<string, string>();
			query["boardId"] = boardId.ToString();
			query["moderatorId"] = moderatorId.ToString();

			// TODO: this shouldn't be a post action if we aren't posting anything
			var response = await _client.PostAsync(QueryHelpers.AddQueryString("Boards/add-moderator", query), null);

			if (response.IsSuccessStatusCode)
			{
				var responseJson = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<GetBoardModel>(responseJson);
			}
			// TODO: Throw some error
			return new GetBoardModel();
		}
	}
}
