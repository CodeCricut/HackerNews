using HackerNews.Api.Controllers.Base;
using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Application.Articles.Commands.VoteArticle;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.ArticleControllers
{
	[Route("api/Articles")]
	public class VoteArticleController : ApiController, IVoteEntityController
	{
		public async Task<ActionResult> VoteEntityAsync(int entityId, bool upvote)
		{
			return Ok(await Mediator.Send(new VoteArticleCommand(entityId, upvote)));
		}
	}
}
