using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.InclusionConfiguration
{
	public interface IEntityInclusionReader<TInclusionConfig, TGetModel> {
		IEnumerable<string> ReadIncludedKeys(TInclusionConfig config);
		IEnumerable<string> ReadIncludedValues(TInclusionConfig config, TGetModel model);
		Dictionary<string, string> ReadIncludedKeyValues(TInclusionConfig config, TGetModel model);

		IEnumerable<string> ReadAllKeys(TInclusionConfig config);
		IEnumerable<string> ReadAllValues(TInclusionConfig config, TGetModel model);
		Dictionary<string, string> ReadAllKeyValues(TInclusionConfig config, TGetModel model);
	}
}
