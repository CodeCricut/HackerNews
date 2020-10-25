using HackerNews.Api.Controllers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.CommentControllers
{
	[Route("api/Comments")]
	public class VoteCommentController : IVoteEntityController
	{
		public Task<ActionResult> VoteEntityAsync(int entityId, [FromBody] bool upvote)
		{
			throw new System.NotImplementedException();
		}
	}
}
