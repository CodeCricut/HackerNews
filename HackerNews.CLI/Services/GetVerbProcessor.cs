using HackerNews.CLI.ProgramServices;
using HackerNews.Domain.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.CLI.Services
{
	public abstract class GetVerbProcessor<TGetModel>
	{
		public async Task ProcessGetVerbOptionsAsync(GetVerbOptions options)
		{
			if (options.Id > 0)
			{
				TGetModel entity = await GetEntityAsync(options.Id);
				OutputEntity(entity);
				return;
			}

			PagingParams pagingParams = new PagingParams();
			if (options.PageNumber > 0) pagingParams.PageNumber = options.PageNumber;
			if (options.PageSize > 0) pagingParams.PageSize = options.PageSize;

			PaginatedList<TGetModel> entityPage;
			if (options.Ids.Count() > 0)
				 entityPage = await GetEntitiesAsync(options.Ids, pagingParams);
			else
				 entityPage = await GetEntitiesAsync(pagingParams);

			OutputEntityPage(entityPage);
		}

		protected abstract Task<PaginatedList<TGetModel>> GetEntitiesAsync(IEnumerable<int> ids, PagingParams pagingParams);

		protected abstract Task<PaginatedList<TGetModel>> GetEntitiesAsync(PagingParams pagingParams);

		protected abstract Task<TGetModel> GetEntityAsync(int id);

		protected abstract void OutputEntityPage(PaginatedList<TGetModel> entityPage);

		protected abstract void OutputEntity(TGetModel entity);
	}
}
