using AutoMapper;
using HackerNews.Domain.Models;
using HackerNews.EF;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNews.Domain;
using Microsoft.AspNetCore.Http;
using HackerNews.Api.Profiles;
using HackerNews.Api.DB_Helpers;

namespace HackerNews.Api.Controllers
{
	[Route("api/[controller]")]
	public class CommentsController : ControllerBase
	{
		private readonly ICommentHelper _commentHelper;

		public CommentsController(ICommentHelper commentHelper)
		{
			_commentHelper = commentHelper;
		}

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

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetCommentAsync(int id)
		{
			try
			{
				var commentModel = await _commentHelper.GetCommentModelAsync(id);

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
