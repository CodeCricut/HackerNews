using HackerNews.Api.Helpers.Objects;
using System.Collections.Generic;

namespace HackerNews.Api.Helpers
{
	public static class ListHelper
	{
		public static List<T> CreateListOfType<T>(int number) where T : new()
		{
			return CreateListOfClones<T>(new T(), number);
		}

		public static List<T> CreateListOfClones<T>(T toClone, int number) where T : new()
		{
			List<T> list = new List<T>();
			for (int i = 0; i < number; i++)
			{
				list.Add(toClone.Copy());
			}
			return list;
		}
	}
}
