using System.Collections.Generic;

namespace HackerNews.CLI.InclusionConfiguration
{
	public interface IEntityInclusionReader<TInclusionConfig, TGetModel>
	{
		IEnumerable<string> ReadIncludedKeys(TInclusionConfig config);
		IEnumerable<string> ReadIncludedValues(TInclusionConfig config, TGetModel model);
		Dictionary<string, string> ReadIncludedKeyValues(TInclusionConfig config, TGetModel model);
	}
}
