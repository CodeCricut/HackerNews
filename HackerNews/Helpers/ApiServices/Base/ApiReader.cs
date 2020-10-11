using CleanEntityArchitecture.Domain;
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

namespace HackerNews.Helpers.ApiServices.Base
{
	public abstract class ApiReader<TGetModel> : IApiReader<TGetModel>
		where TGetModel : GetModelDto, new()
	{
		protected readonly IJwtService _jwtService;
		protected HttpClient _client;

		public ApiReader(IHttpClientFactory clientFactory, IOptions<AppSettings> options, IJwtService jwtService)
		{
			_client = clientFactory.CreateClient();
			// TODO: look into ways of refactoring setup out of constructor
			_client.BaseAddress = new Uri(options.Value.BaseApiAddress);
			_jwtService = jwtService;
		}

		public virtual async Task<IEnumerable<TGetModel>> GetEndpointAsync(string endpoint, PagingParams pagingParams)
		{
			if (_jwtService.ContainsToken())
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtService.GetToken());

			var query = new Dictionary<string, string>();
			if (pagingParams != null &&
				pagingParams.PageNumber > 0 &&
				pagingParams.PageSize > 0)
			{
				query["pageNumber"] = pagingParams.PageNumber.ToString();
				query["pageSize"] = pagingParams.PageSize.ToString();
			}

			var response = await _client.GetAsync(QueryHelpers.AddQueryString(endpoint, query));
			// HTTP GET

			if (response.IsSuccessStatusCode)
			{
				var responseJson = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<IEnumerable<TGetModel>>(responseJson);
			}

			// TODO: Throw some error
			return new List<TGetModel>();
		}

		public virtual async Task<TGetModel> GetEndpointAsync(string endpoint, int id)
		{
			if (_jwtService.ContainsToken())
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtService.GetToken());

			var response = await _client.GetAsync($"{endpoint}/{id}");

			if (response.IsSuccessStatusCode)
			{
				var responseJson = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<TGetModel>(responseJson);
			}
			// TODO: Throw some error
			return new TGetModel();
		}
	}
}
