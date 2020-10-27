﻿using HackerNews.Application.Comments.Commands.AddComment;
using HackerNews.Application.Comments.Commands.AddComments;
using HackerNews.Application.Comments.Commands.DeleteComment;
using HackerNews.Application.Comments.Commands.UpdateComment;
using HackerNews.Application.Comments.Commands.VoteComment;
using HackerNews.Application.Comments.Queries.GetComment;
using HackerNews.Application.Comments.Queries.GetCommentsWithPagination;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Comments;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	[Route("api/[controller]")]
	public class CommentsController : ApiController
	{
		[HttpGet]
		public async Task<ActionResult<PaginatedList<GetCommentModel>>> GetAsync([FromQuery] PagingParams pagingParams)
		{
			return Ok(await Mediator.Send(new GetCommentsWithPaginationQuery(pagingParams)));
		}

		[HttpGet("{key:int}")]
		public async Task<ActionResult<GetCommentModel>> GetByIdAsync(int key)
		{
			return Ok(await Mediator.Send(new GetCommentQuery(key)));
		}

		[HttpPost("vote")]
		public async Task<ActionResult> VoteEntityAsync([FromQuery]  int commentId, [FromQuery] bool upvote)
		{
						return Ok(await Mediator.Send(new VoteCommentCommand(commentId, upvote)));
		}

		[HttpDelete("{key:int}")]
		public async Task<ActionResult> Delete(int key)
		{
						return Ok(await Mediator.Send(new DeleteCommentCommand(key)));
		}

		[HttpPost]
		public async Task<ActionResult<GetCommentModel>> PostAsync([FromBody] PostCommentModel postModel)
		{
						return Ok(await Mediator.Send(new AddCommentCommand(postModel)));
		}

		[HttpPost("range")]
		public async Task<ActionResult> PostRangeAsync([FromBody] IEnumerable<PostCommentModel> postModels)
		{
						return Ok(await Mediator.Send(new AddCommentsCommand(postModels)));
		}

		[HttpPut("{key:int}")]
		public async Task<ActionResult<GetCommentModel>> Put(int key, [FromBody] PostCommentModel updateModel)
		{
			return Ok(await Mediator.Send(new UpdateCommentCommand(key, updateModel)));
		}
	}
}