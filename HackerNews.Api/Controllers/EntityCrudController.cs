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
	public abstract class EntityCrudController<TEntity, TPostEntityModel, TGetEntityModel> : ControllerBase
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
		/// <summary>
		/// Add a valid <paramref name="postModel"/> to the database and return a relevant <typeparamref name="TGetEntityModel"/>.
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns></returns>
		[HttpPost]
		[Authorize]
		public virtual async Task<IActionResult> Post([FromBody] TPostEntityModel postModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			var addedModel = await _entityService.PostEntityModelAsync(postModel, user);

			return Ok(addedModel);
		}

		/// <summary>
		/// Post an array of <typeparamref name="TPostEntityModel"/> to the database. 
		/// </summary>
		/// <param name="postModels"></param>
		/// <returns></returns>
		[HttpPost("range")]
		[Authorize]
		public virtual async Task<IActionResult> PostRange([FromBody] List<TPostEntityModel> postModels)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var user = await _userAuthService.GetAuthenticatedUser(HttpContext);

			await _entityService.PostEntityModelsAsync(postModels, user);

			return Ok();
		}
		#endregion

		#region Read
		/// <summary>
		/// Retrieve a page of <typeparamref name="TGetEntityModel"/> from the database.
		/// </summary>
		/// <param name="pagingParams">For pagination (maximum of 20 documents to a page).</param>
		/// <returns></returns>
		[HttpGet]
		public virtual async Task<IActionResult> Get([FromQuery] PagingParams pagingParams)
		{
			var models = await _entityService.GetAllEntityModelsAsync(pagingParams);
			return Ok(models);
		}

		/// <summary>
		/// Retrieve one <typeparamref name="TGetEntityModel"/> from the database.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[HttpGet("{key:int}")]
		public virtual async Task<IActionResult> GetById(int key)
		{
			var model = await _entityService.GetEntityModelAsync(key);

			return Ok(model);
		}
		#endregion

		#region Update
		/// <summary>
		/// Update the <typeparamref name="TEntity"/> with the given <paramref name="key"/> to have matching properties as <typeparamref name="TPostEntityModel"/>. 
		/// All properties must be included in the <typeparamref name="TPostEntityModel"/>, even if they are the same as the existing document.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="postModel"></param>
		/// <returns></returns>
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
		/// <summary>
		/// Delete the <typeparamref name="TEntity"/> with the given <paramref name="key"/>. For most entities, this will not remove the entity from the database, but 
		/// rather mark a "Deleted" boolean property as true.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
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
