using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Parameters;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HackerNews.Helpers
{
	public abstract class ApiConsumer<TEntity, TPostEntityModel, TGetEntityModel> : IApiConsumer<TEntity, TPostEntityModel, TGetEntityModel>
		where TEntity : DomainEntity
		where TPostEntityModel : PostEntityModel
		where TGetEntityModel : GetEntityModel, new()
	{
		protected readonly HttpClient _client;
		protected readonly AppSettings _settings;

		public ApiConsumer(IHttpClientFactory clientFactory, IOptions<AppSettings> options)
		{
			_client = clientFactory.CreateClient();
			_settings = options.Value;
			_client.BaseAddress = new Uri(_settings.BaseApiAddress);
		}

		public virtual Task<object> DeleteEndpointAsync(string endpoint, int id)
		{
			throw new NotImplementedException();
		}


		public virtual async Task<IEnumerable<TGetEntityModel>> GetEndpointAsync(string endpoint, PagingParams pagingParams)
		{
			var query = new Dictionary<string, string>();
			if (pagingParams != null)
			{
				query["pageNumber"] = pagingParams.PageNumber.ToString();
				query["pageSize"] = pagingParams.PageSize.ToString();
			}

			var response = await _client.GetAsync(QueryHelpers.AddQueryString(endpoint, query));
			// HTTP GET

			if (response.IsSuccessStatusCode)
			{
				var responseJson = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<IEnumerable<TGetEntityModel>>(responseJson);
			}

			// TODO: Throw some error
			return new List<TGetEntityModel>();
		}

		public virtual async Task<TGetEntityModel> GetEndpointAsync(string endpoint, int id)
		{
			var response = await _client.GetAsync($"{endpoint}/{id}");

			if (response.IsSuccessStatusCode)
			{
				var responseJson = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<TGetEntityModel>(responseJson);
			}
			// TODO: Throw some error
			return new TGetEntityModel();
		}

		public virtual async Task<TGetEntityModel> PostEndpointAsync(string endpoint, TPostEntityModel postModel)
		{
			var jsonContent = JsonConvert.SerializeObject(postModel);
			var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

			var response = await _client.PostAsync(endpoint, stringContent);

			if (response.IsSuccessStatusCode)
			{
				var responseJson = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<TGetEntityModel>(responseJson);
			}
			// TODO: throw some error
			return new TGetEntityModel();
		}

		public virtual Task<object> PostEndpointAsync(string endpoint, IEnumerable<TPostEntityModel> postModels)
		{
			throw new NotImplementedException();
		}

		public virtual Task<object> PutEndpointAsync(string endpoint, TPostEntityModel updateModel)
		{
			throw new NotImplementedException();
		}
	}
}
