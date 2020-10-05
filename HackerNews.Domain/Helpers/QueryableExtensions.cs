using HackerNews.Domain.Parameters;
using System.Linq;

namespace HackerNews.Domain.Helpers
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
