using CleanEntityArchitecture.EntityModelServices;
using HackerNews.Api.Controllers.Base;
using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers.ArticleControllers
{
	[Route("api/Articles")]
	public class ModifyArticleController : ModifyEntityController<Article, PostArticleModel, GetArticleModel>
	{
		public ModifyArticleController(IWriteEntityService<Article, PostArticleModel> writeService) : base(writeService)
		{
		}
	}
}
