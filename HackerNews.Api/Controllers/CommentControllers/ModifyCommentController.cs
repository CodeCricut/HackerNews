using HackerNews.Api.Controllers.Base;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Api.Helpers.EntityServices.Interfaces;
using HackerNews.Domain;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers.CommentControllers
{
	[Route("api/Comments")]
	public class ModifyCommentController : ModifyEntityController<Comment, PostCommentModel, GetCommentModel>
	{
		public ModifyCommentController(IModifyEntityService<Comment, PostCommentModel, GetCommentModel> modifyService, IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> userAuthService) : base(modifyService, userAuthService)
		{
		}
	}
}
