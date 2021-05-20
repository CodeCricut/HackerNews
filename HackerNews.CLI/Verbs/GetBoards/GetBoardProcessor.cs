using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Util;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Logging;

namespace HackerNews.CLI.Verbs.GetBoards
{
	public interface IGetBoardProcessor : IGetVerbProcessor<GetBoardModel, GetBoardsOptions>
	{

	}

	public class GetBoardProcessor : GetVerbProcessor<GetBoardModel, GetBoardsOptions>, IGetBoardProcessor
	{
		private readonly IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> _configEntityLogger;
		private readonly IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> _configEntityWriter;
		private readonly ILogger<GetBoardProcessor> _logger;

		public GetBoardProcessor(IGetEntityRepository<GetBoardModel> entityRepository,
			IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> entityLogger,
			IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> entityWriter,
			ILogger<GetBoardProcessor> logger)
			: base(entityRepository, entityLogger, entityWriter, logger)
		{
			_configEntityLogger = entityLogger;
			_configEntityWriter = entityWriter;
			_logger = logger;

			_logger.LogTrace("Created " + this.GetType().Name);
		}

		public override void ConfigureProcessor(GetBoardsOptions options)
		{
			_logger.LogTrace("Configuring " + this.GetType().Name);

			BoardInclusionConfiguration config = options.GetBoardInclusionConfiguration();
			_configEntityLogger.Configure(config);
			_configEntityWriter.Configure(config);
		}
	}
}
