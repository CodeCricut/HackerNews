using HackerNews.Api.Helpers.Attributes;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Api.Helpers.EntityServices;
using HackerNews.Api.Helpers.EntityServices.Base;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	// template for non-odata routes
	[Route("api/[controller]")]
	public class ArticlesController : ODataController
	{
		private readonly ArticleService _articleService;
		private readonly IVoteableEntityService<Article> _articleVoter;
		private readonly UserAuthService _userAuthService;

		public ArticlesController(
			ArticleService articleService,
			IVoteableEntityService<Article> articleVoter,
			UserAuthService userAuthService)
		{
			_articleService = articleService;
			_articleVoter = articleVoter;
			_userAuthService = userAuthService;
		}

		#region Create
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> PostArticleAsync([FromBody] PostArticleModel articleModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var addedModel = await _articleService.PostEntityModelAsync(articleModel, user);

			return Ok(addedModel);
		}

		[HttpPost("range")]
		[Authorize]
		public async Task<IActionResult> PostArticlesAsync([FromBody] List<PostArticleModel> articleModels)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			await _articleService.PostEntityModelsAsync(articleModels, user);

			return Ok();
		}
		#endregion

		#region Read
		[EnableQuery]
		public async Task<IActionResult> GetArticlesAsync()
		{
			var articleModels = await _articleService.GetAllEntityModelsAsync();
			return Ok(articleModels);
		}

		[EnableQuery]
		public async Task<IActionResult> GetArticleAsync(int key)
		{
			var articleModel = await _articleService.GetEntityModelAsync(key);

			return Ok(articleModel);
		}
		#endregion

		#region Update
		[HttpPut("{id:int}")]
		[Authorize]
		public async Task<IActionResult> PutArticleAsync(int id, [FromBody] PostArticleModel articleModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var updatedArticleModel = await _articleService.PutEntityModelAsync(id, articleModel, user);

			return Ok(updatedArticleModel);
		}

		[HttpPost("vote/{articleId:int}")]
		[Authorize]
		public async Task<IActionResult> VoteArticleAsync(int articleId, [FromBody] bool upvote)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			await _articleVoter.VoteEntityAsync(articleId, upvote, user);

			return Ok();
		}
		#endregion

		#region Delete
		[HttpDelete("{id:int}")]
		[Authorize]
		public async Task<IActionResult> DeleteArticleAsync(int key)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			await _articleService.SoftDeleteEntityAsync(key, user);

			return Ok();
		}
		#endregion
	}
}
