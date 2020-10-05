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
		Task<TGetModel> PostAsync([FromBody] TPostModel postModel);

		[HttpPost("range")]
		[Authorize]
		Task PostRangeAsync([FromBody] IEnumerable<TPostModel> postModels);

		[HttpPut("{key:int}")]
		[Authorize]
		Task<TGetModel> Put(int key, [FromBody] TPostModel updateModel);

		[HttpDelete("{key:int}")]
		[Authorize]
		Task Delete(int key);
	}
}
