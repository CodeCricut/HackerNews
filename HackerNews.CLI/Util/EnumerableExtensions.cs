using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Util
{
	public static class EnumerableExtensions
	{
		public static string ToDelimitedList<T>(this IEnumerable<T> list, char delimiter = ',')
		{
			StringBuilder sb = new StringBuilder();
			foreach (T item in list)
				sb.Append($"{item}{delimiter} ");

			return sb.ToString();
		}
	}
}
