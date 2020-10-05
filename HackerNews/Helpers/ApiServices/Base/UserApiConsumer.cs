using HackerNews.Domain;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Helpers
{
	public class UserApiConsumer : ApiConsumer<User, RegisterUserModel, GetPublicUserModel>
	{
		public UserApiConsumer(IHttpClientFactory clientFactory, IOptions<AppSettings> options) : base(clientFactory, options)
		{
		}

		public async Task<GetPrivateUserModel> GetUserByCredentialsAsync(LoginModel authUserReq)
		{
			var jsonContent = JsonConvert.SerializeObject(authUserReq);
			var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

			var response = await _client.PostAsync("users", stringContent);

			if (response.IsSuccessStatusCode)
			{
				var responseJson = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<GetPrivateUserModel>(responseJson);
			}
			// TODO: throw some error
			return new GetPrivateUserModel();
		}

		public async Task<GetPrivateUserModel> GetPrivateUserAsync(string jwtToken)
		{
			// TODO: this works for the lifetime of the context, so put it in the jwt service
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
			var response = await _client.GetAsync("users/me");

			if (response.IsSuccessStatusCode)
			{
				var responseJson = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<GetPrivateUserModel>(responseJson);
			}
			// TODO: throw some error
			return new GetPrivateUserModel();
		}

		public async Task<GetPrivateUserModel> SaveArticleAsync(int articleId, string jwtToken)
		{
			throw new NotImplementedException();
		}

		public async Task<GetPrivateUserModel> SaveCommentAsync(int commentId, string jwtToken)
		{
			throw new NotImplementedException();
		}
	}
}
