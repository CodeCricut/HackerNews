using HackerNews.Api.Controllers.Base;
using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Application.Articles.Queries.GetArticle;
using HackerNews.Application.Articles.Queries.GetArticlesWithPagination;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Articles;
using HackerNews.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.ArticleControllers
{
	[Route("api/Articles")]
	public class ReadArticleController : ApiController, IReadEntityController<Article, GetArticleModel>
	{

		public async Task<ActionResult<PaginatedList<GetArticleModel>>> GetAsync([FromQuery] PagingParams pagingParams)
		{
			return await Mediator.Send(new GetArticlesWithPaginationQuery(pagingParams));
		}

		public async Task<ActionResult<GetArticleModel>> GetByIdAsync(int key)
		{
			return await Mediator.Send(new GetArticleQuery(key));
		}
	}
}
