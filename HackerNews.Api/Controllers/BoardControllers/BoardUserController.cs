using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Api.Helpers.Attributes;
using HackerNews.Api.Helpers.EntityServices.Interfaces;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Board;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.BoardControllers
{
	[Route("api/Boards")]
	public class BoardUserController : ControllerBase, IBoardUserController
	{
		private readonly IBoardUserManagementService _boardUserService;

		public BoardUserController(IBoardUserManagementService boardUserService)
		{
			_boardUserService = boardUserService;
		}

		[HttpPost("add-moderator")]
		[Authorize]
		public async Task<ActionResult<GetBoardModel>> AddModeratorAsync([FromQuery] int boardId, [FromQuery] int moderatorId)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var updatedBoard = await _boardUserService.AddBoardModeratorAsync(boardId, moderatorId);

			return Ok(updatedBoard);
		}

		[HttpPost("add-subscriber")]
		[Authorize]
		public async Task<ActionResult<GetBoardModel>> AddSubscriberAsync([FromQuery] int boardId)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var updatedBoardModel = await _boardUserService.AddBoardSubscriberAsync(boardId);

			return Ok(updatedBoardModel);
		}
	}
}
