using HackerNews.Domain;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers.ApiServices.Base;
using HackerNews.Helpers.Cookies.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace HackerNews.Helpers.ApiServices.Default.UserServices
{
	public class PrivateUserApiModifier : ApiModifier<User, RegisterUserModel, GetPrivateUserModel>
	{
		public PrivateUserApiModifier(IHttpClientFactory clientFactory, IOptions<AppSettings> options, IJwtService jwtService) : base(clientFactory, options, jwtService) { }
	}
}
