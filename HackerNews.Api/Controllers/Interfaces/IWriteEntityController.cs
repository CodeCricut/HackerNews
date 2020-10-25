using HackerNews.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.Interfaces
{
	public interface IWriteEntityController<TEntity, TPostModel, TGetModel>
		where TEntity : DomainEntity
		where TPostModel : PostModelDto
		where TGetModel : GetModelDto
	{
		Task<ActionResult<TGetModel>> PostAsync([FromBody] TPostModel postModel);
		Task<ActionResult> PostRangeAsync([FromBody] IEnumerable<TPostModel> postModels);
		Task<ActionResult<TGetModel>> Put(int key, [FromBody] TPostModel updateModel);
		Task<ActionResult> Delete(int key);
	}
}
