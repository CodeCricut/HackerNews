

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
	public class ArticlesController : ODataController
	{
		private readonly ArticleService _articleService;
		private readonly IVoteableEntityService<Article> _articleVoter;
		private readonly UserAuthService _userAuthService;
		private readonly ILogger<ArticlesController> _logger;

		public ArticlesController(
			ArticleService articleService,
			IVoteableEntityService<Article> articleVoter,
			UserAuthService userAuthService,
			ILogger<ArticlesController> logger)
		{
			_articleService = articleService;
			_articleVoter = articleVoter;
			_userAuthService = userAuthService;
			_logger = logger;
		}

		#region Create
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> PostArticleAsync([FromBody] PostArticleModel articleModel)
		{
			_logger.LogInformation("PostArticleAsync called.");
			if (articleModel == null) _logger.LogWarning("Post model null.");

			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var addedModel = await _articleService.PostEntityModelAsync(articleModel, user);

			return Ok(addedModel);
		}

		[HttpPost("range")]
		[Authorize]
		public async Task<IActionResult> PostArticlesAsync([FromBody] List<PostArticleModel> articleModels)
		{
			_logger.LogInformation("PostArticlesAsync called.");
			if (articleModels == null) _logger.LogWarning("Post models null.");

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
			_logger.LogInformation("GetArticlesAsync called.");
			var articleModels = await _articleService.GetAllEntityModelsAsync();
			return Ok(articleModels);
		}

		[EnableQuery]
		public async Task<IActionResult> GetArticleAsync(int key)
		{
			_logger.LogInformation("GetArticleAsync called.");

			var articleModel = await _articleService.GetEntityModelAsync(key);

			return Ok(articleModel);
		}
		#endregion

		#region Update
		[HttpPut("{id:int}")]
		[Authorize]
		public async Task<IActionResult> PutArticleAsync(int id, [FromBody] PostArticleModel articleModel)
		{
			_logger.LogInformation("PutArticleAsync called.");
			if (id < 1) _logger.LogWarning("Invalid id provided or id didn't bind.");
			if (articleModel == null) _logger.LogWarning("Model null.");

			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var updatedArticleModel = await _articleService.PutEntityModelAsync(id, articleModel, user);

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
		[HttpDelete("{id:int}")]
		[Authorize]
		public async Task<IActionResult> DeleteArticleAsync(int key)
		{
			_logger.LogInformation("DeleteArticleAsync called.");
			if (key < 1) _logger.LogWarning("Invalid id provided or id didn't bind.");

			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			await _articleService.SoftDeleteEntityAsync(key, user);

			return Ok();
		}
		#endregion
	}
}
