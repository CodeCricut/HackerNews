using CleanEntityArchitecture.Domain;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.Helpers.Cookies.Interfaces;
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
		private readonly IJwtService _jwtService;
		protected HttpClient _client;

		public ApiLoginFacilitator(IHttpClientFactory clientFactory, IOptions<AppSettings> options, IJwtService jwtService)
		{
			_client = clientFactory.CreateClient();
			_client.BaseAddress = new Uri(options.Value.BaseApiAddress);
			_jwtService = jwtService;
		}

		public virtual async Task<Jwt> LogIn(TLoginModel authUserReq)
		{
			var jsonContent = JsonConvert.SerializeObject(authUserReq);
			var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

			var response = await _client.PostAsync("jwt", stringContent);

			if (response.IsSuccessStatusCode)
			{
				var responseJson = await response.Content.ReadAsStringAsync();
				var jwt = JsonConvert.DeserializeObject<Jwt>(responseJson);
				_jwtService.SetToken(jwt, 60);

				return jwt;
			}
			// TODO: throw some error
			throw new Exception();
		}
	}
}
