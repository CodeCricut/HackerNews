using HackerNews.Domain.Common.Models;

namespace HackerNews.CLI.Loggers
{
	public interface IEntityLogger<TGetModel>
	{
		void LogEntity(TGetModel entity);
		void LogEntityPage(PaginatedList<TGetModel> entityPage);
	}
}
