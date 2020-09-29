using HackerNews.Api.Helpers.Attributes;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Api.Helpers.EntityServices.Base;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	[Route("api/[controller]")]
	public class CommentsController : ODataController
	{
		private readonly CommentService _commentService;
		private readonly IVoteableEntityService<Comment> _commentVoter;
		private readonly UserAuthService _userAuthService;

		public CommentsController(
			CommentService commentService,
			IVoteableEntityService<Comment> commentVoter,
			UserAuthService userAuthService)
		{
			_commentService = commentService;
			_commentVoter = commentVoter;
			_userAuthService = userAuthService;
		}

		#region Create
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> PostCommentAsync([FromBody] PostCommentModel commentModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var addedModel = await _commentService.PostEntityModelAsync(commentModel, user);

			return Ok(addedModel);
		}

		[HttpPost("range")]
		[Authorize]
		public async Task<IActionResult> PostCommentsAsync([FromBody] List<PostCommentModel> commentModels)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			await _commentService.PostEntityModelsAsync(commentModels, user);

			return Ok();
		}
		#endregion

		#region Read
		[EnableQuery]
		public async Task<IActionResult> GetCommentAsync(int key)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var commentModel = await _commentService.GetEntityModelAsync(key);

			return Ok(commentModel);
		}

		[EnableQuery]
		public async Task<IActionResult> GetCommentsAsync()
		{
			var commentModels = await _commentService.GetAllEntityModelsAsync();

			return Ok(commentModels);
		}
		#endregion

		#region Update
		[HttpPut("{id:int}")]
		[Authorize]
		public async Task<IActionResult> PutCommentAsync(int id, [FromBody] PostCommentModel commentModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var updatedModel = await _commentService.PutEntityModelAsync(id, commentModel, user);

			return Ok(updatedModel);
		}

		[HttpPost("vote/{commentId:int}")]
		[Authorize]
		public async Task<IActionResult> VoteCommentAsync(int commentId, [FromBody] bool upvote)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			await _commentVoter.VoteEntityAsync(commentId, upvote, user);

			return Ok();
		}
		#endregion

		#region Delete
		[HttpDelete("{id:int}")]
		[Authorize]
		public async Task<IActionResult> DeleteCommentAsync(int id)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			await _commentService.SoftDeleteEntityAsync(id, user);

			return Ok();
		}
		#endregion
	}
}
