using HackerNews.Api.Helpers.Attributes;
using HackerNews.Api.Helpers.EntityHelpers;
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
		private readonly IEntityHelper<Article, PostArticleModel, GetArticleModel> _articleHelper;
		private readonly IVoteableEntityHelper<Article> _articleVoter;
		private readonly IAuthenticatableEntityHelper<AuthenticateUserRequest, AuthenticateUserResponse, User, GetPrivateUserModel> _authUserHelper;

		public ArticlesController(IEntityHelper<Article, PostArticleModel, GetArticleModel> articleHelper,
			IVoteableEntityHelper<Article> articleVoter,
			IAuthenticatableEntityHelper<AuthenticateUserRequest, AuthenticateUserResponse, User, GetPrivateUserModel> authUserHelper)
		{
			_articleHelper = articleHelper;
			_articleVoter = articleVoter;
			_authUserHelper = authUserHelper;
		}

		#region Create
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> PostArticleAsync([FromBody] PostArticleModel articleModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _authUserHelper.GetAuthenticatedUser(HttpContext);

			var addedModel = await _articleHelper.PostEntityModelAsync(articleModel, user);

			return Ok(addedModel);
		}

		[HttpPost("range")]
		[Authorize]
		public async Task<IActionResult> PostArticlesAsync([FromBody] List<PostArticleModel> articleModels)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _authUserHelper.GetAuthenticatedUser(HttpContext);

			await _articleHelper.PostEntityModelsAsync(articleModels, user);

			return Ok();
		}
		#endregion

		#region Read
		[EnableQuery]
		public async Task<IActionResult> GetArticlesAsync()
		{
			var articleModels = await _articleHelper.GetAllEntityModelsAsync();
			return Ok(articleModels);
		}

		[EnableQuery]
		public async Task<IActionResult> GetArticleAsync(int key)
		{
			var articleModel = await _articleHelper.GetEntityModelAsync(key);

			return Ok(articleModel);
		}
		#endregion

		#region Update
		[HttpPut("{id:int}")]
		[Authorize]
		public async Task<IActionResult> PutArticleAsync(int id, [FromBody] PostArticleModel articleModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _authUserHelper.GetAuthenticatedUser(HttpContext);

			var updatedArticleModel = await _articleHelper.PutEntityModelAsync(id, articleModel, user);

			return Ok(updatedArticleModel);
		}

		[HttpPost("vote/{articleId:int}")]
		[Authorize]
		public async Task<IActionResult> VoteArticleAsync(int articleId, [FromBody] bool upvote)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _authUserHelper.GetAuthenticatedUser(HttpContext);

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

			var user = await _authUserHelper.GetAuthenticatedUser(HttpContext);

			await _articleHelper.SoftDeleteEntityAsync(key, user);

			return Ok();
		}
		#endregion
	}
}
