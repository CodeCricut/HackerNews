using HackerNews.Domain;
using HackerNews.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Interfaces
{
	public interface IModifyEntityService<TEntity, TPostModel, TGetModel>
		where TEntity : DomainEntity
		where TPostModel : PostEntityModel
		where TGetModel : GetEntityModel
	{
		Task<TGetModel> PostEntityModelAsync(TPostModel entityModel, User currentUser);
		Task PostEntityModelsAsync(IEnumerable<TPostModel> entityModels, User currentUser);
		Task SoftDeleteEntityAsync(int id, User currentUser);
		Task<TGetModel> PutEntityModelAsync(int id, TPostModel entityModel, User currentUser);
	}
}
