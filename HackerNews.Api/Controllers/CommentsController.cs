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
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	[Route("api/[controller]")]
	public class CommentsController : EntityCrudController<Comment, PostCommentModel, GetCommentModel>
	{
		private readonly IVoteableEntityService<Comment> _commentVoter;

		public CommentsController(
			CommentService commentService,
			IVoteableEntityService<Comment> commentVoter, 
			ILogger logger,
			UserAuthService userAuthService) : base(commentService, userAuthService, logger)
		{
			_commentVoter = commentVoter;
		}

		#region Create
		[Authorize]
		public override async Task<IActionResult> PostEntityAsync([FromBody] PostCommentModel postModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var addedModel = await _entityService.PostEntityModelAsync(postModel, user);

			return Ok(addedModel);
		}

		[Authorize]
		public override async Task<IActionResult> PostEntitiesAsync([FromBody] List<PostCommentModel> postModels)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			await _entityService.PostEntityModelsAsync(postModels, user);

			return Ok();
		}
		#endregion

		#region Read
		public override async Task<IActionResult> GetEntitiesAsync()
		{
			var commentModels = await _entityService.GetAllEntityModelsAsync();

			return Ok(commentModels);
		}

		public override async Task<IActionResult> GetEntityAsync(int key)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var commentModel = await _entityService.GetEntityModelAsync(key);

			return Ok(commentModel);
		}
		#endregion

		#region Update
		[Authorize]
		public override async Task<IActionResult> PutEntityAsync(int key, [FromBody] PostCommentModel postModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var updatedModel = await _entityService.PutEntityModelAsync(key, postModel, user);

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
		[Authorize]
		public override async Task<IActionResult> DeleteEntityAsync(int key)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			await _entityService.SoftDeleteEntityAsync(key, user);

			return Ok();
		}
		#endregion
	}
}
