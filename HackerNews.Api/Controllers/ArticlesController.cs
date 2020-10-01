

using HackerNews.Api.Helpers.Attributes;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Api.Helpers.EntityServices;
using HackerNews.Api.Helpers.EntityServices.Base;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Articles;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	// template for non-odata routes
	[Route("api/[controller]")]
	public class ArticlesController : EntityCrudController<Article, PostArticleModel, GetArticleModel>
	{
		private readonly IVoteableEntityService<Article> _articleVoter;

		public ArticlesController(
			ArticleService articleService,
			IVoteableEntityService<Article> articleVoter,
			UserAuthService userAuthService,
			ILogger<ArticlesController> logger) : base(articleService, userAuthService, logger)
		{
			_articleVoter = articleVoter;
		}


		#region Create
		[Authorize]
		public override async Task<IActionResult> PostEntityAsync([FromBody] PostArticleModel postModel)
		{
			if (postModel == null) _logger.LogWarning("Post model null.");

			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var addedModel = await _entityService.PostEntityModelAsync(postModel, user);

			return Ok(addedModel);
		}

		[Authorize]
		public override async Task<IActionResult> PostEntitiesAsync([FromBody] List<PostArticleModel> postModels)
		{
			if (postModels == null) _logger.LogWarning("Post models null.");

			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			await _entityService.PostEntityModelsAsync(postModels, user);

			return Ok();
		}
		#endregion

		#region Read
		public override async Task<IActionResult> GetEntitiesAsync()
		{
			_logger.LogInformation("GetArticlesAsync called.");
			var articleModels = await _entityService.GetAllEntityModelsAsync();
			return Ok(articleModels);
		}

		public override async Task<IActionResult> GetEntityAsync(int key)
		{
			_logger.LogInformation("GetArticleAsync called.");

			var articleModel = await _entityService.GetEntityModelAsync(key);

			return Ok(articleModel);
		}
		#endregion

		#region Update
		[Authorize]
		public override async Task<IActionResult> PutEntityAsync(int key, [FromBody] PostArticleModel articleModel)
		{
			_logger.LogInformation("PutArticleAsync called.");
			if (key < 1) _logger.LogWarning("Invalid id provided or id didn't bind.");
			if (articleModel == null) _logger.LogWarning("Model null.");

			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var updatedArticleModel = await _entityService.PutEntityModelAsync(key, articleModel, user);

			return Ok(updatedArticleModel);
		}

		[HttpPost("vote/{articleId:int}")]
		[Authorize]
		public async Task<IActionResult> VoteArticleAsync(int articleId, [FromBody] bool upvote)
		{
			_logger.LogInformation("VoteArticleAsync called.");
			if (articleId < 1) _logger.LogWarning("Invalid id provided or id didn't bind.");

			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			await _articleVoter.VoteEntityAsync(articleId, upvote, user);

			return Ok();
		}
		#endregion

		#region Delete
		[Authorize]
		public override async Task<IActionResult> DeleteEntityAsync(int key)
		{
			_logger.LogInformation("DeleteArticleAsync called.");
			if (key < 1) _logger.LogWarning("Invalid id provided or id didn't bind.");

			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			await _entityService.SoftDeleteEntityAsync(key, user);

			return Ok();
		}
		#endregion
	}
}
