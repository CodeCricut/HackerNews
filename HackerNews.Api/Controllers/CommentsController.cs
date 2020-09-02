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

		// add include children bool param
		public async Task<IActionResult> Get()
		{
			try
			{
				var commentModels = await _commentHelper.GetAllCommentModels();

				return Ok(commentModels);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var commentModel = await _commentHelper.GetCommentModel(id);

				return Ok(commentModel);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost]
		public async Task<IActionResult> PostComment([FromBody] PostCommentModel commentModel)
		{
			try
			{
				if (!ModelState.IsValid) throw new Exception("Model invalid");

				await _commentHelper.PostCommentModel(commentModel);
				
				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}



		[HttpPut("{id:int}")]
		public async Task<IActionResult> PutComment(int id, [FromBody] PostCommentModel commentModel)
		{
			try
			{
				if (!ModelState.IsValid) throw new Exception("Model invalid");

				var updatedModel = await _commentHelper.PutCommentModel(id, commentModel);

				return Ok(updatedModel);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteComment(int id)
		{
			try
			{
				await _commentHelper.DeleteComment(id);
				
				return Ok();
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost("vote/{commentId:int}")]
		public async Task<IActionResult> VoteComment(int commentId, [FromBody] bool upvote)
		{
			try
			{
				await _commentHelper.VoteComment(commentId, upvote);

				return Ok();
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
