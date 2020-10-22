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

namespace HackerNews.Helpers.ApiServices.Base
{
	public class ApiReader : IApiReader
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

		public virtual async Task<PagedListResponse<TGetModel>> GetEndpointAsync<TGetModel>(string endpoint, PagingParams pagingParams, bool includeDeleted = false) where TGetModel : GetModelDto, new()
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
				var responsePage = JsonConvert.DeserializeObject<PagedListResponse<TGetModel>>(responseJson);
				if (responsePage != null && !includeDeleted)
					responsePage.Items = responsePage.Items.Where(item => !item.Deleted);
				return responsePage;
			}

			// TODO: Throw some error
			return null;
		}


		public virtual async Task<TGetModel> GetEndpointAsync<TGetModel>(string endpoint, bool includeDeleted = false) where TGetModel : GetModelDto, new()
		{
			var jwt = _jwtService.GetToken();
			return await GetEndpointAsync<TGetModel>(endpoint, jwt);
		}

		// needed for a VERY ugly workaround, where the jwt cannot be accessed from the cookie store in the same request as it was set
		public virtual async Task<TGetModel> GetEndpointAsync<TGetModel>(string endpoint, string jwt, bool includeDeleted = false) where TGetModel : GetModelDto, new()
		{
			if (jwt.Length >= 0) _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

			var response = await _client.GetAsync($"{endpoint}");

			if (response.IsSuccessStatusCode)
			{
				var responseJson = await response.Content.ReadAsStringAsync();
				var responseObj = JsonConvert.DeserializeObject<TGetModel>(responseJson);
				if (responseObj != null && !includeDeleted && responseObj.Deleted)
					return new TGetModel();
				return responseObj;
			}
			// TODO: Throw some error
			return new TGetModel();
		}


		public virtual async Task<TGetModel> GetEndpointAsync<TGetModel>(string endpoint, int id, bool includeDeleted = false) where TGetModel : GetModelDto, new()
		{
			if (_jwtService.ContainsToken())
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtService.GetToken());

			var response = await _client.GetAsync($"{endpoint}/{id}");

			if (response.IsSuccessStatusCode)
			{
				var responseJson = await response.Content.ReadAsStringAsync();

				var responseObj = JsonConvert.DeserializeObject<TGetModel>(responseJson);
				if (responseObj != null && responseObj.Deleted && !includeDeleted)
					return new TGetModel();
				return responseObj;
			}
			// TODO: Throw some error
			throw new Exception();
		}

		public async Task<PagedListResponse<TGetModel>> GetEndpointAsync<TGetModel>(string endpoint, IEnumerable<int> ids, PagingParams pagingParams, bool includeDeleted = false) where TGetModel : GetModelDto, new()
		{
			var idPage = ids.Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize).Take(pagingParams.PageSize);

			IEnumerable<TGetModel> enumerable = await GetEndpointAsync<TGetModel>(endpoint, idPage, includeDeleted);
			PagedList<TGetModel> pagedList = new PagedList<TGetModel>(enumerable.ToList(), ids.Count(), pagingParams);

			PagedListResponse<TGetModel> pagedListResponse = new PagedListResponse<TGetModel>(pagedList);
			if (pagedListResponse != null && !includeDeleted)
				pagedListResponse.Items = pagedListResponse.Items.Where(item => !item.Deleted);

			return pagedListResponse;
		}

		public async Task<IEnumerable<TGetModel>> GetEndpointAsync<TGetModel>(string endpoint, IEnumerable<int> ids, bool includeDeleted = false) where TGetModel : GetModelDto, new()
		{
			if (_jwtService.ContainsToken())
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtService.GetToken());

			var queryString = "?";

			if (ids.Count() > 0)
			{
				var idArray = ids.ToArray();
				queryString += $"id={idArray[0]}";

				for (int i = 1; i < idArray.Count(); i++)
				{
					queryString += $"&id={idArray[i]}";
				}
			}


			var response = await _client.GetAsync($"{endpoint}/range{queryString}");

			if (response.IsSuccessStatusCode)
			{
				var responseJson = await response.Content.ReadAsStringAsync();

				var responseList = JsonConvert.DeserializeObject<IEnumerable<TGetModel>>(responseJson);
				if (responseList != null && !includeDeleted)
					responseList = responseList.Where(item => !item.Deleted);

				return responseList;
			}
			// TODO: Throw some error
			return null;
		}



		public async Task<PagedListResponse<TGetModel>> GetEndpointWithQueryAsync<TGetModel>(string endpoint, string query, PagingParams pagingParams, bool includeDeleted = false) where TGetModel : GetModelDto, new()
		{
			if (_jwtService.ContainsToken())
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtService.GetToken());

			var queryString = new Dictionary<string, string>();
			if (pagingParams != null &&
				pagingParams.PageNumber > 0 &&
				pagingParams.PageSize > 0)
			{
				queryString["pageNumber"] = pagingParams.PageNumber.ToString();
				queryString["pageSize"] = pagingParams.PageSize.ToString();
			}

			queryString["query"] = query;

			var response = await _client.GetAsync(QueryHelpers.AddQueryString($"{endpoint}/search", queryString));
			// HTTP GET

			if (response.IsSuccessStatusCode)
			{
				var responseJson = await response.Content.ReadAsStringAsync();

				var pagedListResponse = JsonConvert.DeserializeObject<PagedListResponse<TGetModel>>(responseJson);
				if (pagedListResponse != null && !includeDeleted)
					pagedListResponse.Items = pagedListResponse.Items.Where(item => !item.Deleted);
				return pagedListResponse;
			}

			// TODO: Throw some error
			return null;
		}
	}
}
