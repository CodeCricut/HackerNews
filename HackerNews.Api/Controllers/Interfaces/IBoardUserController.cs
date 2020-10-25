using HackerNews.Application.Common.Models.Boards;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.Interfaces
{
	public interface IBoardUserController
	{
		public Task<ActionResult<GetBoardModel>> AddSubscriberAsync([FromQuery] int boardId);
		public Task<ActionResult<GetBoardModel>> AddModeratorAsync([FromQuery] int boardId, [FromQuery] int moderatorId);
	}
}
