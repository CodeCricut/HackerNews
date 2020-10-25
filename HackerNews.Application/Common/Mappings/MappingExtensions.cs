using AutoMapper;
using HackerNews.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Application.Common.Mappings
{
	public static class MappingExtensions
	{
		public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, PagingParams pagingParams)
			=> PaginatedList<TDestination>.CreateAsync(queryable, pagingParams.PageNumber, pagingParams.PageSize);

		public static PaginatedList<TDestination> ToMappedPagedList<TSource, TDestination>(this PaginatedList<TSource> list, IMapper mapper)
		{
			IEnumerable<TDestination> sourceList = mapper.Map<IEnumerable<TDestination>>(list.Items);
			PaginatedList<TDestination> pagedResult = new PaginatedList<TDestination>(sourceList.ToList(), list.TotalCount, list.PageIndex, list.PageSize);
			return pagedResult;
		}
	}
}
