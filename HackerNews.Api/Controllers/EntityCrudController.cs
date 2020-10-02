using AutoMapper;
using HackerNews.Api.Helpers.Attributes;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Api.Helpers.EntityServices.Base;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models;
using HackerNews.Domain.Parameters;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	public abstract class EntityCrudController<TEntity, TPostEntityModel, TGetEntityModel> : ODataController
		where TEntity : DomainEntity
		where TPostEntityModel : PostEntityModel
		where TGetEntityModel : GetEntityModel
	{
		protected readonly EntityService<TEntity, TPostEntityModel, TGetEntityModel> _entityService;
		protected readonly UserAuthService _userAuthService;
		protected readonly ILogger _logger;

		public EntityCrudController(EntityService<TEntity, TPostEntityModel, TGetEntityModel> entityService, UserAuthService userAuthService, ILogger logger)
		{
			_entityService = entityService;
			_userAuthService = userAuthService;
			_logger = logger;
		}

		#region Create
		[HttpPost]
		[Authorize]
		public virtual async Task<IActionResult> Post([FromBody] TPostEntityModel postModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var addedModel = await _entityService.PostEntityModelAsync(postModel, user);

			return Ok(addedModel);
		}

		[HttpPost("range")]
		[Authorize]
		public virtual async Task<IActionResult> Post([FromBody] List<TPostEntityModel> postModels)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			await _entityService.PostEntityModelsAsync(postModels, user);

			return Ok();
		}
		#endregion

		#region Read
		[EnableQuery]
		[HttpGet]
		public virtual async Task<IActionResult> Get([FromQuery] PagingParams pagingParams)
		{
			var models = await _entityService.GetAllEntityModelsAsync(pagingParams);
			return Ok(models);
		}

		// For some reason, putting [EnableQuery] results in an ambiguous match exception (even though OData should be able to distinguish due to the key param)
		[HttpGet("{key:int}")]
		public virtual async Task<IActionResult> Get(int key)
		{
			var model = await _entityService.GetEntityModelAsync(key);

			return Ok(model);
		}
		#endregion

		#region Update
		[HttpPut("{key:int}")]
		[Authorize]
		public virtual async Task<IActionResult> Put(int key, [FromBody] TPostEntityModel postModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var updatedModel = await _entityService.PutEntityModelAsync(key, postModel, user);

			return Ok(updatedModel);
		}
		#endregion

		#region Delete
		[Authorize]
		[HttpDelete("{key:int}")]
		public virtual async Task<IActionResult> Delete( int key)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			await _entityService.SoftDeleteEntityAsync(key, user);

			return Ok();
		}
		#endregion
	}
}
