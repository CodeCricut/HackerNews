using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Util
{
	public static class StringExtensions
	{
		public static string Quote(this string s)
		{
			return $"\"{s}\"";
		}
	}
}
