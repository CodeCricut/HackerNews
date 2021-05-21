using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

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
