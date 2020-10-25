using HackerNews.Api.Controllers.Base;
using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Boards;
using HackerNews.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.BoardControllers
{
	[Route("api/Boards")]
	public class ReadBoardController : ApiController, IReadEntityController<Board, GetBoardModel>
	{
		public async Task<ActionResult<PaginatedList<GetBoardModel>>> GetAsync([FromQuery] PagingParams pagingParams)
		{
			// todo
			throw new System.NotImplementedException();
		}

		public async Task<ActionResult<GetBoardModel>> GetByIdAsync(int key)
		{
			throw new System.NotImplementedException();
		}
	}
}
