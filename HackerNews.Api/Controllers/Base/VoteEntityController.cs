using CleanEntityArchitecture.Domain;
using HackerNews.Api.Helpers.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.Base
{
	public abstract class VoteEntityController<TEntity> : ControllerBase
		where TEntity : DomainEntity
	{
		[HttpPost("vote/{entityId:int}")]
		[Authorize]
		public abstract Task<IActionResult> VoteEntityAsync(int entityId, [FromBody] bool upvote);
	}
}
