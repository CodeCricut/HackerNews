using HackerNews.Api.Helpers.Attributes;
using HackerNews.Api.Helpers.EntityServices.Base;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Board;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	[Route("api/[controller]")]
	public class BoardsController : EntityCrudController<Board, PostBoardModel, GetBoardModel>
	{
		private readonly BoardUserManagmentService _boardUserService;

		public BoardsController(BoardService boardService,
			UserAuthService userAuthService,
			BoardUserManagmentService boardUserService,
			ILogger<BoardsController> logger) : base(boardService, userAuthService, logger)
		{
			_boardUserService = boardUserService;
		}

		#region Create
		#endregion

		#region Read
		#endregion

		#region Update

		[HttpPost("add-subscriber")]
		[Authorize]
		public async Task<IActionResult> AddSubscriberAsync([FromQuery] int boardId)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var updatedBoardModel = await _boardUserService.AddBoardSubscriberAsync(boardId, user);

			return Ok(updatedBoardModel);
		}

		// not a great URI scheme
		[HttpPost("add-moderator")]
		[Authorize]
		public async Task<IActionResult> AddModeratorAsync([FromQuery] int boardId, [FromQuery] int moderatorId)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);
			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var updatedBoard = await _boardUserService.AddBoardModeratorAsync(boardId, user, moderatorId);

			return Ok(updatedBoard);
		}
		#endregion

		#region Delete
		#endregion
	}
}
