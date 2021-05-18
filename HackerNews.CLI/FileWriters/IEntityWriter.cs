using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.FileWriters
{
	public interface IEntityWriter<TGetModel>
	{
		Task WriteEntityAsync(string fileLoc, TGetModel entity);
		Task WriteEntityPageAsync(string fileLoc, PaginatedList<TGetModel> entityPage);
		void Configure(BoardInclusionConfiguration config);
	}
}
