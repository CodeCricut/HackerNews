using HackerNews.Domain.Common.Models;
using System.Linq;

namespace HackerNews.Domain.Common.Helpers
{
	public static class QueryableExtensions
	{
		public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PagingParams pagingParams)
		{
			return queryable.Skip(pagingParams.PageSize * (pagingParams.PageNumber - 1))
					.Take(pagingParams.PageSize);
		}
	}
}
