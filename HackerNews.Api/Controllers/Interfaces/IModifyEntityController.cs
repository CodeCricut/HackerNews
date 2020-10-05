using HackerNews.Api.Helpers.Attributes;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.Interfaces
{
	public interface IModifyEntityController<TEntity, TPostModel, TGetModel>
		where TEntity : DomainEntity
		where TPostModel : PostEntityModel
		where TGetModel : GetEntityModel
	{
		[HttpPost]
		[Authorize]
		Task<ActionResult<TGetModel>> PostAsync([FromBody] TPostModel postModel);

		[HttpPost("range")]
		[Authorize]
		Task<ActionResult> PostRangeAsync([FromBody] IEnumerable<TPostModel> postModels);

		[HttpPut("{key:int}")]
		[Authorize]
		Task<ActionResult<TGetModel>> Put(int key, [FromBody] TPostModel updateModel);

		[HttpDelete("{key:int}")]
		[Authorize]
		Task<ActionResult> Delete(int key);
	}
}
