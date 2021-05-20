using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Util;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Boards;

namespace HackerNews.CLI.Verbs.GetBoards
{
	public interface IGetBoardProcessor : IGetVerbProcessor<GetBoardModel, GetBoardsOptions>
	{

	}

	public class GetBoardProcessor : GetVerbProcessor<GetBoardModel, GetBoardsOptions>, IGetBoardProcessor
	{
		private readonly IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> _configEntityLogger;
		private readonly IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> _configEntityWriter;

		public GetBoardProcessor(IGetEntityRepository<GetBoardModel> entityRepository,
			IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> entityLogger,
			IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> entityWriter)
			: base(entityRepository, entityLogger, entityWriter)
		{
			_configEntityLogger = entityLogger;
			_configEntityWriter = entityWriter;
		}

		public override void ConfigureProcessor(GetBoardsOptions options)
		{
			BoardInclusionConfiguration config = options.GetBoardInclusionConfiguration();
			_configEntityLogger.Configure(config);
			_configEntityWriter.Configure(config);
		}
	}
}
