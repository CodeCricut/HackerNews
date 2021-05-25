using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Logging;

namespace HackerNews.CLI.Loggers
{
	public class ConfigurableBoardLogger :
		ConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration>
	{
		public ConfigurableBoardLogger(ILogger<ConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration>> logger, IEntityInclusionReader<BoardInclusionConfiguration, GetBoardModel> articleInclusionReader, BoardInclusionConfiguration inclusionConfig) : base(logger, articleInclusionReader, inclusionConfig)
		{
		}

		protected override string GetEntityName()
			=> "Board";

		protected override string GetEntityNamePlural()
			=> "Boards";
	}
}
