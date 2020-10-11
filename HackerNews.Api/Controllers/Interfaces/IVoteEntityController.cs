using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.Interfaces
{
	public interface IVoteEntityController
	{
		public abstract Task<ActionResult> VoteEntityAsync(int entityId, [FromBody] bool upvote);
	}
}
