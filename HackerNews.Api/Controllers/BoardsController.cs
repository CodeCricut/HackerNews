using HackerNews.Application.Boards.Commands.AddBoard;
using HackerNews.Application.Boards.Commands.AddBoards;
using HackerNews.Application.Boards.Commands.AddModerator;
using HackerNews.Application.Boards.Commands.AddSubscriber;
using HackerNews.Application.Boards.Commands.DeleteBoard;
using HackerNews.Application.Boards.Commands.UpdateBoard;
using HackerNews.Application.Boards.Queries.GetBoard;
using HackerNews.Application.Boards.Queries.GetBoardsByIds;
using HackerNews.Application.Boards.Queries.GetBoardsBySearch;
using HackerNews.Application.Boards.Queries.GetBoardsWithPagination;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Exceptions;
using HackerNews.Web.Pipeline.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	[Route("api/[controller]")]
	public class BoardsController : ApiController
	{
		[HttpDelete("{key:int}")]
		[JwtAuthorize]
		public async Task<ActionResult> Delete(int key)
		{
			return Ok(await Mediator.Send(new DeleteBoardCommand(key)));
		}

		[HttpPost]
		[JwtAuthorize]
		public async Task<ActionResult<GetBoardModel>> PostAsync([FromBody] PostBoardModel postModel)
		{
			return Ok(await Mediator.Send(new AddBoardCommand(postModel)));
		}

		[HttpPost("range")]
		[JwtAuthorize]
		public async Task<ActionResult> PostRangeAsync([FromBody] IEnumerable<PostBoardModel> postModels)
		{
			return Ok(await Mediator.Send(new AddBoardsCommand(postModels)));

		}

		[HttpPut("{key:int}")]
		[JwtAuthorize]
		public async Task<ActionResult<GetBoardModel>> Put(int key, [FromBody] PostBoardModel updateModel)
		{
			return Ok(await Mediator.Send(new UpdateBoardCommand(key, updateModel)));
		}

		[HttpGet]
		public async Task<ActionResult<PaginatedList<GetBoardModel>>> GetAsync([FromQuery] PagingParams pagingParams)
		{
			return Ok(await Mediator.Send(new GetBoardsWithPaginationQuery(pagingParams)));
		}

		[HttpGet("range")]
		public async Task<ActionResult<PaginatedList<GetBoardModel>>> GetByIdAsync([FromQuery] IEnumerable<int> id, [FromQuery] PagingParams pagingParams)
		{
			return await Mediator.Send(new GetBoardsByIdsQuery(id, pagingParams));
		}

		[HttpGet("search")]
		public async Task<ActionResult<PaginatedList<GetBoardModel>>> Search(string searchTerm, PagingParams pagingParams)
		{
			return await Mediator.Send(new GetBoardsBySearchQuery(searchTerm, pagingParams));
		}

		[HttpGet("{key:int}")]
		public async Task<ActionResult<GetBoardModel>> GetByIdAsync(int key)
		{
			return Ok(await Mediator.Send(new GetBoardQuery(key)));
		}

		[HttpPost("add-moderator")]
		[JwtAuthorize]
		public async Task<ActionResult<GetBoardModel>> AddModeratorAsync([FromQuery] int boardId, [FromQuery] int moderatorId)
		{
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
