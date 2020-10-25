using HackerNews.Api.Controllers.Base;
using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Api.Pipeline.Filters;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.UserControllers
{
	[Route("api/Users")]
	public class UserSaverController : ApiController, IUserSaverController
	{
		[HttpPost("save-article")]
		[JwtAuthorize]
		public async Task<ActionResult<GetPrivateUserModel>> SaveArticleAsync([FromQuery] int articleId)
		{
			throw new NotImplementedException();
		}

		[HttpPost("save-comment")]
		[JwtAuthorize]
		public async Task<ActionResult<GetPrivateUserModel>> SaveCommentAsync([FromQuery] int commentId)
		{
			throw new NotImplementedException();
		}
	}
}
