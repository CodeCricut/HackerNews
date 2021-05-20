using HackerNews.Domain.Common.Models;
using System.Threading.Tasks;

namespace HackerNews.CLI.FileWriters
{
	public interface IEntityWriter<TGetModel>
	{
		Task WriteEntityAsync(string fileLoc, TGetModel entity);
		Task WriteEntityPageAsync(string fileLoc, PaginatedList<TGetModel> entityPage);
	}

	public interface IConfigurableEntityWriter<TGetModel, TIncludeConfiguration> : IEntityWriter<TGetModel>
	{
		void Configure(TIncludeConfiguration config); 
	}
}
