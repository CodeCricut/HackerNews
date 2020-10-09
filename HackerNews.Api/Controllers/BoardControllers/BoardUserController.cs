using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Api.Helpers.Attributes;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Api.Helpers.EntityServices.Interfaces;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.BoardControllers
{
	[Route("api/Boards")]
	public class BoardUserController : ControllerBase, IBoardUserController
	{
		private readonly IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> _userAuthService;
		private readonly IBoardUserManagementService _boardUserService;

		public BoardUserController(IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> userAuthService,
			IBoardUserManagementService boardUserService)
		{
			_userAuthService = userAuthService;
			_boardUserService = boardUserService;
		}

		[HttpPost("add-moderator")]
		[Authorize]
		public async Task<ActionResult<GetBoardModel>> AddModeratorAsync([FromQuery] int boardId, [FromQuery] int moderatorId)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);
			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var updatedBoard = await _boardUserService.AddBoardModeratorAsync(boardId, user, moderatorId);

			return Ok(updatedBoard);
		}

		[HttpPost("add-subscriber")]
		[Authorize]
		public async Task<ActionResult<GetBoardModel>> AddSubscriberAsync([FromQuery] int boardId)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var updatedBoardModel = await _boardUserService.AddBoardSubscriberAsync(boardId, user);

			return Ok(updatedBoardModel);
		}
	}
}
