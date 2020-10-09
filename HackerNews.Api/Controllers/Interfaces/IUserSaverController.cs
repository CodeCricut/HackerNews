using HackerNews.Api.Helpers.Attributes;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.Interfaces
{
	public interface IUserSaverController
	{
		public Task<ActionResult<GetPrivateUserModel>> SaveArticleAsync([FromQuery] int articleId);
		public Task<ActionResult<GetPrivateUserModel>> SaveCommentAsync([FromQuery] int commentId);
	}
}
