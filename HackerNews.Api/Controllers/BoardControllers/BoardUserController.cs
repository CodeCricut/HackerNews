using HackerNews.Api.Controllers.Base;
using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Api.Pipeline.Filters;
using HackerNews.Application.Boards.Commands.AddModerator;
using HackerNews.Application.Boards.Commands.AddSubscriber;
using HackerNews.Application.Common.Models.Boards;
using HackerNews.Domain.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.BoardControllers
{
	[Route("api/Boards")]
	public class BoardUserController : ApiController, IBoardUserController
	{

		public BoardUserController()
		{
		}

		[HttpPost("add-moderator")]
		[JwtAuthorize]
		public async Task<ActionResult<GetBoardModel>> AddModeratorAsync([FromQuery] int boardId, [FromQuery] int moderatorId)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			return Ok(await Mediator.Send(new AddModeratorCommand(boardId, moderatorId)));
		}

		[HttpPost("add-subscriber")]
		[JwtAuthorize]
		public async Task<ActionResult<GetBoardModel>> AddSubscriberAsync([FromQuery] int boardId)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			return Ok(await Mediator.Send(new AddSubscriberCommand(boardId)));
		}
	}
}
