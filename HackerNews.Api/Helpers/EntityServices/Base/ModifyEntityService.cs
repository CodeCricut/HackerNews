using HackerNews.Api.DB_Helpers;
using HackerNews.Api.Helpers.EntityServices.Interfaces;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base
{
	public abstract class ModifyEntityService<TEntity, TPostModel, TGetModel> : IModifyEntityService<TEntity, TPostModel, TGetModel>
		where TEntity : DomainEntity
		where TPostModel : PostEntityModel
		where TGetModel : GetEntityModel
	{
		public abstract Task<TGetModel> PostEntityModelAsync(TPostModel entityModel, Domain.User currentUser);

		public virtual async Task PostEntityModelsAsync(IEnumerable<TPostModel> entityModels, User currentUser)
		{
			await TaskHelper.RunConcurrentTasksAsync<TPostModel, TGetModel>(entityModels, postModel => PostEntityModelAsync(postModel, currentUser));
		}

		public abstract Task<TGetModel> PutEntityModelAsync(int id, TPostModel entityModel, Domain.User currentUser);
		public abstract Task SoftDeleteEntityAsync(int id, Domain.User currentUser);
	}
}
