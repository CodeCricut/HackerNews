using CleanEntityArchitecture.Controllers;
using CleanEntityArchitecture.EntityModelServices;
using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers.ArticleControllers
{
	[Route("api/Articles")]
	public class WriteArticleController : WriteController<Article, PostArticleModel, GetArticleModel>
	{
		public WriteArticleController(IWriteEntityService<Article, PostArticleModel> writeService) : base(writeService)
		{
		}
	}
}
