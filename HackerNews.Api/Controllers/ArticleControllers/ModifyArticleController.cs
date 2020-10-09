using HackerNews.Api.Controllers.Base;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Api.Helpers.EntityServices.Interfaces;
using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers.ArticleControllers
{
	[Route("api/Articles")]
	public class ModifyArticleController : ModifyEntityController<Article, PostArticleModel, GetArticleModel>
	{
		public ModifyArticleController(IModifyEntityService<Article, PostArticleModel, GetArticleModel> modifyService,
			IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> userAuthService) : base(modifyService, userAuthService)
		{
		}
	}
}
