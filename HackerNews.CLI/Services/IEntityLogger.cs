using HackerNews.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Services
{
	public interface IEntityLogger<TGetModel>
	{
		void LogEntity(TGetModel entity);
		void LogEntityPage(PaginatedList<TGetModel> entityPage);
	}
}
