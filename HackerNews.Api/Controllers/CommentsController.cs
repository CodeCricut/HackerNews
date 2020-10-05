using HackerNews.Api.Helpers.Attributes;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Api.Helpers.EntityServices.Base.UserServices;
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
			ILogger<CommentsController> logger,
			UserAuthService userAuthService) : base(commentService, userAuthService, logger)
		{
			_commentVoter = commentVoter;
		}

		#region Create
		#endregion

		#region Read
		#endregion

		#region Update
		/// <summary>
		/// Vote on the <see cref="Comment"/> with the Id = <paramref name="commentId"/>. 
		/// </summary>
		/// <param name="commentId"></param>
		/// <param name="upvote">True if upvote, false if downvote</param>
		/// <returns></returns>
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
		#endregion
	}
}
