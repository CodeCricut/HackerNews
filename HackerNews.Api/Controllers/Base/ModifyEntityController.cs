using CleanEntityArchitecture.Domain;
using CleanEntityArchitecture.EntityModelServices;
using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Api.Helpers.Attributes;
using HackerNews.Domain.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.Base
{
	public abstract class ModifyEntityController<TEntity, TPostModel, TGetModel> : ControllerBase, IModifyEntityController<TEntity, TPostModel, TGetModel>
		where TEntity : DomainEntity
		where TPostModel : PostModelDto
		where TGetModel : GetModelDto
	{
		protected readonly IWriteEntityService<TEntity, TPostModel> _writeService;

		public ModifyEntityController(IWriteEntityService<TEntity, TPostModel> writeService)
		{
			_writeService = writeService;
		}

		[HttpPost]
		[Authorize]
		public virtual async Task<ActionResult<TGetModel>> PostAsync([FromBody] TPostModel postModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var addedModel = await _writeService.PostEntityModelAsync<TGetModel>(postModel);

			return Ok(addedModel);
		}

		[HttpPost("range")]
		[Authorize]
		public virtual async Task<ActionResult> PostRangeAsync([FromBody] IEnumerable<TPostModel> postModels)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			await _writeService.PostEntityModelsAsync(postModels.ToList());

			return Ok();
		}

		[HttpPut("{key:int}")]
		[Authorize]
		public virtual async Task<ActionResult<TGetModel>> Put(int key, TPostModel updateModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);


			var updatedModel = await _writeService.PutEntityModelAsync<TGetModel>(key, updateModel);

			return Ok(updatedModel);
		}

		[HttpDelete("{key:int}")]
		[Authorize]
		public virtual async Task<ActionResult> Delete(int key)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			await _writeService.SoftDeleteEntityAsync(key);

			return Ok();
		}
	}
}
