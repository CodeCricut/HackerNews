using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.InclusionConfiguration
{
	public interface IEntityReader<TGetModel>
	{
		IEnumerable<string> ReadAllKeys();
		IEnumerable<string> ReadAllValues(TGetModel model);
		Dictionary<string, string> ReadAllKeyValues(TGetModel model);
	}
}
