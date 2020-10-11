using CleanEntityArchitecture.Domain;
using HackerNews.Helpers.ApiServices.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Base
{
	public abstract class ApiLoginFacilitator<TLoginModel, TGetModel> : IApiLoginFacilitator<TLoginModel, TGetModel>
		where TGetModel : GetModelDto, new()
	{
		protected HttpClient _client;

		public ApiLoginFacilitator(IHttpClientFactory clientFactory, IOptions<AppSettings> options)
		{
			_client = clientFactory.CreateClient();
			_client.BaseAddress = new Uri(options.Value.BaseApiAddress);
		}

		public virtual async Task<TGetModel> GetUserByCredentialsAsync(TLoginModel authUserReq)
		{
			var jsonContent = JsonConvert.SerializeObject(authUserReq);
			var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

			var response = await _client.PostAsync("users", stringContent);

			if (response.IsSuccessStatusCode)
			{
				var responseJson = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<TGetModel>(responseJson);
			}
			// TODO: throw some error
			return new TGetModel();
		}
	}
}
