using CleanEntityArchitecture.Domain;
using HackerNews.Domain.Models.Users;
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
		private readonly ICookieUserManager _cookieUserManager;
		private readonly IApiReader _apiReader;
		protected HttpClient _client;

		public ApiLoginFacilitator(IHttpClientFactory clientFactory, IOptions<AppSettings> options, IJwtService jwtService, ICookieUserManager cookieUserManager, IApiReader apiReader)
		{
			_client = clientFactory.CreateClient();
			_client.BaseAddress = new Uri(options.Value.BaseApiAddress);
			_jwtService = jwtService;
			_cookieUserManager = cookieUserManager;
			_apiReader = apiReader;
		}

		public virtual async Task<Jwt> LogIn(TLoginModel authUserReq)
		{
			if (_jwtService.ContainsToken()) _jwtService.RemoveToken();

			var jsonContent = JsonConvert.SerializeObject(authUserReq);
			var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

			var response = await _client.PostAsync("jwt", stringContent);

			if (response.IsSuccessStatusCode)
			{
				var responseJson = await response.Content.ReadAsStringAsync();
				var jwt = JsonConvert.DeserializeObject<Jwt>(responseJson);
				_jwtService.SetToken(jwt, 60);

				// Get user then attach it to the cookies
				var user = await _apiReader.GetEndpointAsync<GetPrivateUserModel>("users/me", jwt.Token);
				await _cookieUserManager.LogInAsync(user);

				return jwt;
			}
			// TODO: throw some error
			throw new Exception();
		}

		public async Task LogOut()
		{
			if (_jwtService.ContainsToken())
			{
				_jwtService.RemoveToken();
				await _cookieUserManager.LogOutAsync();
			}
		}
	}
}
