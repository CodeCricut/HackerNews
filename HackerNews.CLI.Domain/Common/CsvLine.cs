using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackerNews.CLI.Output.FileWriters
{
	public class CsvLine
	{
		public List<string> Items { get; set; } = new List<string>();

		public CsvLine()
		{

		}

		private CsvLine(IEnumerable<string> items)
		{
			Items = items.ToList();
		}

		public static CsvLine FromLines(IEnumerable<string> items)
		{
			return new CsvLine(items);
		}

		public string ToDelimitedList(char delimiter = ',')
		{
			StringBuilder sb = new StringBuilder();
			foreach (var item in Items)
				sb.Append($"{item}{delimiter}");

			return sb.ToString();
		}
	}
}
