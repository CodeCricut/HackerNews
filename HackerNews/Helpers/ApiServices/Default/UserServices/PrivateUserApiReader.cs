using HackerNews.Helpers.ApiServices.Base;
using HackerNews.Helpers.Cookies.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace HackerNews.Helpers.ApiServices.Default.UserServices
{
	public class PrivateUserApiReader : ApiReader
	{
		public PrivateUserApiReader(IHttpClientFactory clientFactory, IOptions<AppSettings> options, IJwtService jwtService) : base(clientFactory, options, jwtService)
		{
		}

		//public override async Task<GetPrivateUserModel> GetEndpointAsync<GetPrivateUserModel>(string endpoint, int id, bool includeDeleted = false)
		//{
		//	if (_jwtService.ContainsToken())
		//		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtService.GetToken());

		//	// don't include ID
		//	var response = await _client.GetAsync($"{endpoint}");

		//	if (response.IsSuccessStatusCode)
		//	{
		//		var responseJson = await response.Content.ReadAsStringAsync();
		//		return JsonConvert.DeserializeObject<GetPrivateUserModel>(responseJson);
		//	}
		//	// TODO: Throw some error
		//	throw new System.Exception();
		//}
	}
}
