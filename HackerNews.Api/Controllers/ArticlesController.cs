using HackerNews.Api.Pipeline.Filters;
using HackerNews.Application.Articles.Commands.AddArticle;
using HackerNews.Application.Articles.Commands.AddArticles;
using HackerNews.Application.Articles.Commands.DeleteArticle;
using HackerNews.Application.Articles.Commands.UpdateArticle;
using HackerNews.Application.Articles.Commands.VoteArticle;
using HackerNews.Application.Articles.Queries.GetArticle;
using HackerNews.Application.Articles.Queries.GetArticlesByIds;
using HackerNews.Application.Articles.Queries.GetArticlesWithPagination;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Articles;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	[Route("api/[controller]")]
	public class ArticlesController : ApiController
	{
		[HttpGet]
		public async Task<ActionResult<PaginatedList<GetArticleModel>>> GetAsync([FromQuery] PagingParams pagingParams)
		{
			return await Mediator.Send(new GetArticlesWithPaginationQuery(pagingParams));
		}

		[HttpGet("range")]
		public async Task<ActionResult<PaginatedList<GetArticleModel>>> GetByIdAsync([FromQuery] IEnumerable<int> id, [FromQuery] PagingParams pagingParams)
		{
			return await Mediator.Send(new GetArticlesByIdsQuery(id, pagingParams));
		}

		[HttpGet("{key:int}")]
		public async Task<ActionResult<GetArticleModel>> GetByIdAsync(int key)
		{
			return await Mediator.Send(new GetArticleQuery(key));
		}

		[HttpPost("vote")]
		[JwtAuthorize]
		public async Task<ActionResult> VoteEntityAsync([FromQuery] int articleId, [FromQuery] bool upvote)
		{
			return Ok(await Mediator.Send(new VoteArticleCommand(articleId, upvote)));
		}

		[HttpDelete("{key:int}")]
		[JwtAuthorize]
		public async Task<ActionResult> Delete(int key)
		{
			return Ok(await Mediator.Send(new DeleteArticleCommand(key)));
		}

		[HttpPost]
		[JwtAuthorize]
		public async Task<ActionResult<GetArticleModel>> PostAsync([FromBody] PostArticleModel postModel)
		{
			return Ok(await Mediator.Send(new AddArticleCommand(postModel)));
		}

		[HttpPost("range")]
		[JwtAuthorize]
		public async Task<ActionResult> PostRangeAsync([FromBody] IEnumerable<PostArticleModel> postModels)
		{
			return Ok(await Mediator.Send(new AddArticlesCommand(postModels)));
		}

		[HttpPut("{key:int}")]
		[JwtAuthorize]
		public async Task<ActionResult<GetArticleModel>> Put(int key, [FromBody] PostArticleModel updateModel)
		{
			return Ok(await Mediator.Send(new UpdateArticleCommand(key, updateModel)));
		}
	}
}
