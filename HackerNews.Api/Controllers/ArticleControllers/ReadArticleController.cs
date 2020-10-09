using HackerNews.Api.Controllers.Base;
using HackerNews.Api.Helpers.EntityServices.Interfaces;
using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers.ArticleControllers
{
	[Route("api/Articles")]
	public class ReadArticleController : ReadEntityController<Article, GetArticleModel>
	{
		public ReadArticleController(IReadEntityService<Article, GetArticleModel> readService) : base(readService)
		{
		}
	}
}
