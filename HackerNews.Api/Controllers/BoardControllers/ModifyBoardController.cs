using HackerNews.Api.Controllers.Base;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Api.Helpers.EntityServices.Interfaces;
using HackerNews.Domain;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers.BoardControllers
{
	[Route("api/Boards")]
	public class ModifyBoardController : ModifyEntityController<Board, PostBoardModel, GetBoardModel>
	{
		public ModifyBoardController(IModifyEntityService<Board, PostBoardModel, GetBoardModel> modifyService, IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> userAuthService) : base(modifyService, userAuthService)
		{
		}
	}
}
