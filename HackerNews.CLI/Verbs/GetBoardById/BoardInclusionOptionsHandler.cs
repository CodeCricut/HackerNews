using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Verbs.GetBoards;
using HackerNews.Domain.Common.Models.Boards;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.GetBoardById
{
	public class BoardInclusionOptionsHandlerFactory
	{
		public BoardInclusionOptionsHandlerFactory(/* non-instance dependencies */)
		{

		}

		public BoardInclusionOptionsHandler Create(
			IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> configLogger,
			IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> configWriter)
		{
			return new BoardInclusionOptionsHandler(configLogger, configWriter);
		}
	}

	public class BoardInclusionOptionsHandler
	{
		private readonly IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> _configLogger;
		private readonly IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> _configWriter;

		public BoardInclusionOptionsHandler(IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> configLogger,
			IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> configWriter)
		{
			_configLogger = configLogger;
			_configWriter = configWriter;
		}

		public void Handle(IBoardInclusionOptions options)
		{
			_configLogger.Configure(options.GetInclusionConfiguration());
			_configWriter.Configure(options.GetInclusionConfiguration());
		}
	}
}
