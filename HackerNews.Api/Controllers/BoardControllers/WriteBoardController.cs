using HackerNews.Api.Controllers.Base;
using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Application.Common.Models.Boards;
using HackerNews.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.BoardControllers
{
	[Route("api/Boards")]
	public class WriteBoardController : ApiController, IWriteEntityController<Board, PostBoardModel, GetBoardModel>
	{
		public Task<ActionResult> Delete(int key)
		{
			throw new System.NotImplementedException();
		}

		public Task<ActionResult<GetBoardModel>> PostAsync([FromBody] PostBoardModel postModel)
		{
			throw new System.NotImplementedException();
		}

		public Task<ActionResult> PostRangeAsync([FromBody] IEnumerable<PostBoardModel> postModels)
		{
			throw new System.NotImplementedException();
		}

		public Task<ActionResult<GetBoardModel>> Put(int key, [FromBody] PostBoardModel updateModel)
		{
			throw new System.NotImplementedException();
		}
	}
}
