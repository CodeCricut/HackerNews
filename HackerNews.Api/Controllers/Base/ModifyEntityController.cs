using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Api.Helpers.Attributes;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Api.Helpers.EntityServices.Interfaces;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.Base
{
	public abstract class ModifyEntityController<TEntity, TPostModel, TGetModel> : ControllerBase, IModifyEntityController<TEntity, TPostModel, TGetModel>
		where TEntity : DomainEntity
		where TPostModel : PostEntityModel
		where TGetModel : GetEntityModel
	{
		protected readonly IModifyEntityService<TEntity, TPostModel, TGetModel> _modifyService;
		protected readonly IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> _userAuthService;

		public ModifyEntityController(IModifyEntityService<TEntity, TPostModel, TGetModel> modifyService, IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel> userAuthService)
		{
			_modifyService = modifyService;
			_userAuthService = userAuthService;
		}

		[HttpPost]
		[Authorize]
		public virtual async Task<ActionResult<TGetModel>> PostAsync([FromBody] TPostModel postModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var addedModel = await _modifyService.PostEntityModelAsync(postModel, user);

			return Ok(addedModel);
		}
		
		[HttpPost("range")]
		[Authorize]
		public virtual async Task<ActionResult> PostRangeAsync([FromBody] IEnumerable<TPostModel> postModels)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			await _modifyService.PostEntityModelsAsync(postModels, user);

			return Ok();
		}

		[HttpPut("{key:int}")]
		[Authorize]
		public virtual async Task<ActionResult<TGetModel>> Put(int key, TPostModel updateModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var updatedModel = await _modifyService.PutEntityModelAsync(key, updateModel, user);

			return Ok(updatedModel);
		}

		[HttpDelete("{key:int}")]
		[Authorize]
		public virtual async Task<ActionResult> Delete(int key)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			await _modifyService.SoftDeleteEntityAsync(key, user);

			return Ok();
		}
	}
}
