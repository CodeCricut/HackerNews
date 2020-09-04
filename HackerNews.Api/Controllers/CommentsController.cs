using HackerNews.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using HackerNews.Api.DB_Helpers;
using System.Collections.Generic;
using Microsoft.AspNet.OData;
using HackerNews.Domain.Errors;
using HackerNews.Api.Helpers.EntityHelpers;

namespace HackerNews.Api.Controllers
{
	[Route("api/[controller]")]
	public class CommentsController : ODataController 
	{
		private readonly CommentHelper _commentHelper;

		public CommentsController(CommentHelper commentHelper)
		{
			_commentHelper = commentHelper;
		}

		#region Create
		[HttpPost]
		public async Task<IActionResult> PostCommentAsync([FromBody] PostCommentModel commentModel)
		{
				if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

				var addedModel = await _commentHelper.PostEntityModelAsync(commentModel);

				return Ok(addedModel);
		}

		[HttpPost("range")]
		public async Task<IActionResult> PostCommentsAsync([FromBody] List<PostCommentModel> commentModels)
		{
				if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

				await _commentHelper.PostEntityModelsAsync(commentModels);

				return Ok();
		}
		#endregion

		#region Read
		[EnableQuery]
		public async Task<IActionResult> GetCommentAsync(int key)
		{
				var commentModel = await _commentHelper.GetEntityModelAsync(key);

				return Ok(commentModel);
		}

		[EnableQuery]
		public async Task<IActionResult> GetCommentsAsync()
		{
				var commentModels = await _commentHelper.GetAllEntityModelsAsync();

				return Ok(commentModels);
		}
		#endregion

		#region Update
		[HttpPut("{id:int}")]
		public async Task<IActionResult> PutCommentAsync(int id, [FromBody] PostCommentModel commentModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

				var updatedModel = await _commentHelper.PutEntityModelAsync(id, commentModel);

				return Ok(updatedModel);
		}

		[HttpPost("vote/{commentId:int}")]
		public async Task<IActionResult> VoteCommentAsync(int commentId, [FromBody] bool upvote)
		{
				await _commentHelper.VoteEntityAsync(commentId, upvote);

				return Ok();
		}
		#endregion

		#region Delete
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteCommentAsync(int id)
		{
				await _commentHelper.SoftDeleteEntityAsync(id);

				return Ok();
		}
		#endregion
	}
}
