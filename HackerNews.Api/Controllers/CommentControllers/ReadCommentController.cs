using HackerNews.Api.Controllers.Base;
using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Comments;
using HackerNews.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.CommentControllers
{
	[Route("api/Comments")]
	public class ReadCommentController : ApiController, IReadEntityController<Comment, GetCommentModel>
	{
		public Task<ActionResult<PaginatedList<GetCommentModel>>> GetAsync([FromQuery] PagingParams pagingParams)
		{
			throw new System.NotImplementedException();
		}

		public Task<ActionResult<GetCommentModel>> GetByIdAsync(int key)
		{
			throw new System.NotImplementedException();
		}
	}
}
