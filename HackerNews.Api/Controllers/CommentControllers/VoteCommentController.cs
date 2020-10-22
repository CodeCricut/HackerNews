using HackerNews.Api.Controllers.Base;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.CommentControllers
{
	[Route("api/Comments")]
	public class VoteCommentController : VoteEntityController<Comment>
	{
		private readonly IVoteableEntityService<Comment> _commentVoter;

		public VoteCommentController(IVoteableEntityService<Comment> commentVoter)
		{
			_commentVoter = commentVoter;
		}

		/// <summary>
		/// Vote on the <see cref="Comment"/> with the Id = <paramref name="commentId"/>. 
		/// </summary>
		/// <param name="commentId"></param>
		/// <param name="upvote">True if upvote, false if downvote</param>
		/// <returns></returns>
		public override async Task<IActionResult> VoteEntityAsync(int entityId, [FromBody] bool upvote)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			await _commentVoter.VoteEntityAsync(entityId, upvote);

			return Ok();
		}
	}
}
