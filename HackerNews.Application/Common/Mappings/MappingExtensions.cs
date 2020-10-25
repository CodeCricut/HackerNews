using HackerNews.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Application.Common.Mappings
{
	public static class MappingExtensions
	{
		public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, PagingParams pagingParams)
			=> PaginatedList<TDestination>.CreateAsync(queryable, pagingParams.PageNumber, pagingParams.PageSize);
	}
}
