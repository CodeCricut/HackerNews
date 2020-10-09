using HackerNews.Domain.Models.Users;
using HackerNews.Helpers.ApiServices.Base;
using HackerNews.Helpers.Cookies.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Default.UserServices
{
	public class PublicUserApiReader : ApiReader<GetPublicUserModel>
	{
		public PublicUserApiReader(IHttpClientFactory clientFactory, IOptions<AppSettings> options, IJwtService jwtService) : base(clientFactory, options, jwtService)
		{
		}
	}
}
