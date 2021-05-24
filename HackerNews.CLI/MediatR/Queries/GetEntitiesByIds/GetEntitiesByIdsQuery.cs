using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR.Queries.GetEntitiesByIds
{
	public class GetEntitiesByIdsQuery<TGetModel> : IRequest<PaginatedList<TGetModel>>
		where TGetModel : GetModelDto
	{
		public GetEntitiesByIdsQuery(IIdListOptions idsOptions,
			IPageOptions pageOptions)
		{
			IdsOptions = idsOptions;
			PageOptions = pageOptions;
		}

		public IIdListOptions IdsOptions { get; }
		public IPageOptions PageOptions { get; }
	}

	public abstract class GetEntitiesByIdsQueryHandler<TGetModel> 
		where TGetModel : GetModelDto
	{
		private readonly IEntityFinder<TGetModel> _entityFinder;

		public GetEntitiesByIdsQueryHandler(IEntityFinder<TGetModel> entityFinder)
		{
			_entityFinder = entityFinder;
		}

		public Task<PaginatedList<TGetModel>> GetEntitiesByIdsAsync(GetEntitiesByIdsQuery<TGetModel> request)
		{
			// TODO: create extension method
			var pagingParams = new PagingParams(request.PageOptions.PageNumber, request.PageOptions.PageNumber);
			return _entityFinder.GetByIdsAsync(request.IdsOptions.Ids, pagingParams);
		}
	}
}
