using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.Domain.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Interfaces
{
	interface IReadEntityService<TEntity, TGetModel>
		where TEntity : DomainEntity
		where TGetModel : GetEntityModel
	{
		Task<TGetModel> GetEntityModelAsync(int id);
		Task<PagedList<TGetModel>> GetAllEntityModelsAsync(PagingParams pagingParams);
	}
}
