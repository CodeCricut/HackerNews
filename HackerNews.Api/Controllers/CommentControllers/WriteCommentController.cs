using HackerNews.Api.Controllers.Base;
using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Application.Common.Models.Comments;
using HackerNews.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.CommentControllers
{
	[Route("api/Comments")]
	public class WriteCommentController : ApiController, IWriteEntityController<Comment, PostCommentModel, GetCommentModel>
	{
		public Task<ActionResult> Delete(int key)
		{
			throw new System.NotImplementedException();
		}

		public Task<ActionResult<GetCommentModel>> PostAsync([FromBody] PostCommentModel postModel)
		{
			throw new System.NotImplementedException();
		}

		public Task<ActionResult> PostRangeAsync([FromBody] IEnumerable<PostCommentModel> postModels)
		{
			throw new System.NotImplementedException();
		}

		public Task<ActionResult<GetCommentModel>> Put(int key, [FromBody] PostCommentModel updateModel)
		{
			throw new System.NotImplementedException();
		}
	}
}
