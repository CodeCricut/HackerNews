using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.GetEntity
{
	public interface IGetVerbProcessor<GetModel, TOptions>

		where TOptions : IGetEntityVerbOptions
	{
		void ConfigureProcessor(TOptions options);
		Task ProcessGetVerbOptionsAsync(TOptions options);
	}

	public abstract class GetVerbProcessor<TGetModel, TOptions> :
		IGetVerbProcessor<TGetModel, TOptions>
		where TOptions : IGetEntityVerbOptions
	{
		protected IGetEntityRepository<TGetModel> EntityRepository { get; private set; }
		protected IEntityLogger<TGetModel> EntityLogger { get; private set; }
		protected IEntityWriter<TGetModel> EntityWriter { get; private set; }

		public GetVerbProcessor(
			IGetEntityRepository<TGetModel> entityRepository,
			IEntityLogger<TGetModel> entityLogger,
			IEntityWriter<TGetModel> entityWriter)
		{
			EntityRepository = entityRepository;
			EntityLogger = entityLogger;
			EntityWriter = entityWriter;
		}

		public async Task ProcessGetVerbOptionsAsync(TOptions options)
		{
			ConfigureProcessor(options);

			if (options.Id > 0)
			{
				TGetModel entity = await EntityRepository.GetByIdAsync(options.Id);
				OutputEntity(options, entity);
				return;
			}

			PagingParams pagingParams = new PagingParams();
			if (options.PageNumber > 0) pagingParams.PageNumber = options.PageNumber;
			if (options.PageSize > 0) pagingParams.PageSize = options.PageSize;

			PaginatedList<TGetModel> entityPage;
			if (options.Ids.Count() > 0)
				entityPage = await EntityRepository.GetByIdsAsync(options.Ids, pagingParams);
			else
				entityPage = await EntityRepository.GetPageAsync(pagingParams);

			OutputEntityPage(options, entityPage);
		}


		protected virtual void OutputEntityPage(TOptions options, PaginatedList<TGetModel> entityPage)
		{
			if (options.Print)
				EntityLogger.LogEntityPage(entityPage);
			if (!string.IsNullOrEmpty(options.File))
				EntityWriter.WriteEntityPageAsync(options.File, entityPage);
		}

		protected virtual void OutputEntity(TOptions options, TGetModel entity)
		{
			if (options.Print)
				EntityLogger.LogEntity(entity);
			if (!string.IsNullOrEmpty(options.File))
				EntityWriter.WriteEntityAsync(options.File, entity);
		}

		public abstract void ConfigureProcessor(TOptions options);
	}
}
