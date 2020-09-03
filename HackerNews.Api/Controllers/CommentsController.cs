using HackerNews.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using HackerNews.Api.DB_Helpers;
using System.Collections.Generic;
using Microsoft.AspNet.OData;

namespace HackerNews.Api.Controllers
{
	[Route("api/[controller]")]
	public class CommentsController : ODataController 
	{
		private readonly ICommentHelper _commentHelper;

		public CommentsController(ICommentHelper commentHelper)
		{
			_commentHelper = commentHelper;
		}

		[EnableQuery]
		public async Task<IActionResult> GetCommentsAsync()
		{
			try
			{
				var commentModels = await _commentHelper.GetAllCommentModelsAsync();

				return Ok(commentModels);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[EnableQuery]
		public async Task<IActionResult> GetCommentAsync(int key)
		{
			try
			{
				var commentModel = await _commentHelper.GetCommentModelAsync(key);

				return Ok(commentModel);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost]
		public async Task<IActionResult> PostCommentAsync([FromBody] PostCommentModel commentModel)
		{
			try
			{
				if (!ModelState.IsValid) throw new Exception("Model invalid");

				var addedModel = await _commentHelper.PostCommentModelAsync(commentModel);
				
				return Ok(addedModel);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost("range")]
		public async Task<IActionResult> PostCommentsAsync([FromBody] List<PostCommentModel> commentModels)
		{
			try
			{
				if (!ModelState.IsValid) throw new Exception("Model invalid");

				await _commentHelper.PostCommentModelsAsync(commentModels);

				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}



		[HttpPut("{id:int}")]
		public async Task<IActionResult> PutCommentAsync(int id, [FromBody] PostCommentModel commentModel)
		{
			try
			{
				if (!ModelState.IsValid) throw new Exception("Model invalid");

				var updatedModel = await _commentHelper.PutCommentModelAsync(id, commentModel);

				return Ok(updatedModel);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteCommentAsync(int id)
		{
			try
			{
				await _commentHelper.DeleteCommentAsync(id);
				
				return Ok();
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost("vote/{commentId:int}")]
		public async Task<IActionResult> VoteCommentAsync(int commentId, [FromBody] bool upvote)
		{
			try
			{
				await _commentHelper.VoteCommentAsync(commentId, upvote);

				return Ok();
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
