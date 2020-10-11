using CleanEntityArchitecture.Domain;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.Helpers.Cookies.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Base
{
	public abstract class ApiModifier<TEntity, TPostModel, TGetModel> : IApiModifier<TEntity, TPostModel, TGetModel>
		where TEntity : DomainEntity
		where TPostModel : PostModelDto
		where TGetModel : GetModelDto, new()
	{
		protected readonly IJwtService _jwtService;
		protected HttpClient _client;

		public ApiModifier(IHttpClientFactory clientFactory, IOptions<AppSettings> options, IJwtService jwtService)
		{
			_client = clientFactory.CreateClient();
			_client.BaseAddress = new Uri(options.Value.BaseApiAddress);
			_jwtService = jwtService;
		}

		public virtual Task<bool> DeleteEndpointAsync(string endpoint, int id)
		{
			throw new NotImplementedException();
		}

		public virtual async Task<TGetModel> PostEndpointAsync(string endpoint, TPostModel postModel)
		{
			// try attatch JWT token if present
			if (_jwtService.ContainsToken())
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtService.GetToken());

			var jsonContent = JsonConvert.SerializeObject(postModel);
			var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

			var response = await _client.PostAsync(endpoint, stringContent);

			if (response.IsSuccessStatusCode)
			{
				var responseJson = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<TGetModel>(responseJson);
			}
			// TODO: throw some error
			return new TGetModel();
		}

		public virtual Task<bool> PostEndpointAsync(string endpoint, IEnumerable<TPostModel> postModels)
		{
			// try attatch JWT token if present
			if (_jwtService.ContainsToken())
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtService.GetToken());

			throw new NotImplementedException();
		}

		public virtual Task<TGetModel> PutEndpointAsync(string endpoint, TPostModel updateModel)
		{
			// try attatch JWT token if present
			if (_jwtService.ContainsToken())
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtService.GetToken());

			throw new NotImplementedException();
		}
	}
}
