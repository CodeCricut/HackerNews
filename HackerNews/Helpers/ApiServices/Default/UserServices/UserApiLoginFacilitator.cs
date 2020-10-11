using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers.ApiServices.Base;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace HackerNews.Helpers.ApiServices.Default.UserServices
{
	public class UserApiLoginFacilitator : ApiLoginFacilitator<LoginModel, GetPrivateUserModel>
	{
		public UserApiLoginFacilitator(IHttpClientFactory clientFactory, IOptions<AppSettings> options) : base(clientFactory, options)
		{
		}
	}
}
