using HackerNews.Api.DB_Helpers;
using HackerNews.Api.Helpers.EntityServices.Interfaces;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base
{
	public abstract class ModifyEntityService<TEntity, TPostModel, TGetModel> : IModifyEntityService<TEntity, TPostModel, TGetModel>
		where TEntity : DomainEntity
		where TPostModel : PostEntityModel
		where TGetModel : GetEntityModel
	{
		public abstract Task<TGetModel> PostEntityModelAsync(TPostModel entityModel, Domain.User currentUser);
		public async Task PostEntityModelsAsync(List<TPostModel> entityModels, Domain.User currentUser)
		{
			await TaskHelper.RunConcurrentTasksAsync(entityModels, postModel => PostEntityModelAsync(postModel, currentUser));
		}
		public abstract Task<TGetModel> PutEntityModelAsync(int id, TPostModel entityModel, Domain.User currentUser);
		public abstract Task SoftDeleteEntityAsync(int id, Domain.User currentUser);
	}
}
