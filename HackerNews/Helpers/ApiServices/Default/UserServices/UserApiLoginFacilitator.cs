using CleanEntityArchitecture.Domain;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers.ApiServices.Base;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.Helpers.Cookies.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace HackerNews.Helpers.ApiServices.Default.UserServices
{
	public class UserApiLoginFacilitator : ApiLoginFacilitator<LoginModel, GetPrivateUserModel>
	{
		public UserApiLoginFacilitator(IHttpClientFactory clientFactory, IOptions<AppSettings> options, IJwtService jwtService, ICookieUserManager cookieUserManager, IApiReader apiReader) : base(clientFactory, options, jwtService, cookieUserManager, apiReader)
		{
		}
	}
}
