using HackerNews.Domain;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers.ApiServices.Interfaces;
using System;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Default.UserServices
{
	public class ApiUserSaver : IApiUserSaver<Article, GetPrivateUserModel>, IApiUserSaver<Comment, GetPrivateUserModel>
	{
		Task<GetPrivateUserModel> IApiUserSaver<Article, GetPrivateUserModel>.SaveEntityToUserAsync(int articleId)
		{
			throw new NotImplementedException();
		}

		Task<GetPrivateUserModel> IApiUserSaver<Comment, GetPrivateUserModel>.SaveEntityToUserAsync(int commentId)
		{
			throw new NotImplementedException();
		}
	}
}
