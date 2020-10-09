using HackerNews.Api.Helpers.Attributes;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.Interfaces
{
	public interface IModifyEntityController<TEntity, TPostModel, TGetModel>
		where TEntity : DomainEntity
		where TPostModel : PostEntityModel
		where TGetModel : GetEntityModel
	{
		Task<ActionResult<TGetModel>> PostAsync([FromBody] TPostModel postModel);
		Task<ActionResult> PostRangeAsync([FromBody] IEnumerable<TPostModel> postModels);
		Task<ActionResult<TGetModel>> Put(int key, [FromBody] TPostModel updateModel);
		Task<ActionResult> Delete(int key);
	}
}
