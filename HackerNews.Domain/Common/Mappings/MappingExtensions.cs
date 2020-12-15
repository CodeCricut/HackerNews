using AutoMapper;
using HackerNews.Domain.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Domain.Common.Mappings
{
	public static class MappingExtensions
	{
		/// <summary>
		/// Paginate a queryable source.
		/// </summary>
		/// <typeparam name="TDestination"></typeparam>
		/// <param name="queryable"></param>
		/// <param name="pagingParams"></param>
		/// <returns></returns>
		public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, PagingParams pagingParams)
			=> PaginatedList<TDestination>.CreateAsync(queryable, pagingParams.PageNumber, pagingParams.PageSize);

		/// <summary>
		/// Map each item in the <paramref name="list"/> of type <typeparamref name="TSource"/> to a new item of type <typeparamref name="TDestination"/>,
		/// otherwise maintaining other properties of the list.
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="TDestination"></typeparam>
		/// <param name="list"></param>
		/// <param name="mapper"></param>
		/// <returns></returns>
		public static PaginatedList<TDestination> ToMappedPagedList<TSource, TDestination>(this PaginatedList<TSource> list, IMapper mapper)
		{
			IEnumerable<TDestination> sourceList = mapper.Map<IEnumerable<TDestination>>(list.Items);
			PaginatedList<TDestination> pagedResult = new PaginatedList<TDestination>(sourceList.ToList(), list.TotalCount, list.PageIndex, list.PageSize);
			return pagedResult;
		}
	}
}
