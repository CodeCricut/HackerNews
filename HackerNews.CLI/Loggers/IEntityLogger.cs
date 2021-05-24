using HackerNews.Domain.Common.Models;

namespace HackerNews.CLI.Loggers
{
	public interface IEntityLogger<TGetModel>
	{
		void LogEntity(TGetModel entity);
		void LogEntityPage(PaginatedList<TGetModel> entityPage);
	}

	public interface IConfigurableEntityLogger<TGetModel, TIncludeConfiguration> : IEntityLogger<TGetModel>
	{
		void Configure(TIncludeConfiguration config);
	}
}
