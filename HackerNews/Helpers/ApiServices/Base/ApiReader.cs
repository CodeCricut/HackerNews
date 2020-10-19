using CleanEntityArchitecture.Domain;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.Helpers.Cookies.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace HackerNews.Helpers.ApiServices.Base
{
	public class ApiReader: IApiReader
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

		public virtual async Task<PagedListResponse<TGetModel>> GetEndpointAsync<TGetModel>(string endpoint, PagingParams pagingParams) where TGetModel : GetModelDto, new()
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
				return JsonConvert.DeserializeObject<PagedListResponse<TGetModel>>(responseJson);
			}

			// TODO: Throw some error
			return null;
		}


		public virtual async Task<TGetModel> GetEndpointAsync<TGetModel>(string endpoint) where TGetModel : GetModelDto, new()
		{
			if (_jwtService.ContainsToken())
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtService.GetToken());

			var response = await _client.GetAsync($"{endpoint}");

			if (response.IsSuccessStatusCode)
			{
				var responseJson = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<TGetModel>(responseJson);
			}
			// TODO: Throw some error
			return new TGetModel();
		}

		public virtual async Task<TGetModel> GetEndpointAsync<TGetModel>(string endpoint, int id) where TGetModel : GetModelDto, new()
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
			throw new Exception();
		}

		public async Task<IEnumerable<TGetModel>> GetEndpointAsync<TGetModel>(string endpoint, IEnumerable<int> ids) where TGetModel : GetModelDto, new()
		{
			if (_jwtService.ContainsToken())
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtService.GetToken());




			//var query = new Dictionary<string, string>();
			//if (pagingParams != null &&
			//	pagingParams.PageNumber > 0 &&
			//	pagingParams.PageSize > 0)
			//{
			//	query["pageNumber"] = pagingParams.PageNumber.ToString();
			//	query["pageSize"] = pagingParams.PageSize.ToString();
			//}

			//var response = await _client.GetAsync(QueryHelpers.AddQueryString(endpoint, query));

			var queryString = "?";

			if (ids.Count() > 0)
			{
				var idArray = ids.ToArray();
				queryString += $"id={idArray[0]}";

				for(int i = 1; i < idArray.Count(); i++)
				{
					queryString += $"&id={idArray[i]}";
				}
			}

			//var query = HttpUtility.ParseQueryString(string.Empty);
			//foreach (var id in ids)
			//	query["id"] = id.ToString();

			//var queryString = query.ToString();

			var response = await _client.GetAsync($"{endpoint}/range{queryString}");

			if (response.IsSuccessStatusCode)
			{
				var responseJson = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<IEnumerable<TGetModel>>(responseJson);
			}
			// TODO: Throw some error
			return null;
		}
	}
}
