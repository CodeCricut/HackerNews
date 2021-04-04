using Hackernews.WPF.ApiClients;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackernews.WPF.Services
{
	public interface ISignInManager
	{
		public Task SignInAsync(LoginModel loginModel);
	}

	public class WpfSignInManager : ISignInManager
	{
		private readonly IJwtPrincipal _jwtPrincipal;
		private readonly IApiClient _apiClient;

		public WpfSignInManager(IJwtPrincipal jwtPrincipal, IApiClient apiClient)
		{
			_jwtPrincipal = jwtPrincipal;
			_apiClient = apiClient;
		}

		public async Task SignInAsync(LoginModel loginModel)
		{
			var jwt = await _apiClient.PostAsync<LoginModel, Jwt>(loginModel, "account/login");
			_jwtPrincipal.SetJwt(jwt);
		}
	}
}
