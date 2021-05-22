using HackerNews.CLI.Configuration;
using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Logging;
namespace HackerNews.CLI.Verbs.GetBoardById
{
	public class GetBoardByIdRequestFactory
	{
		private readonly ILogger<GetBoardByIdRequest> _logger;
		private readonly IVerbositySetter _verbositySetter;
		private readonly IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> _entityLogger;
		private readonly IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> _entityWriter;
		private readonly IGetEntityRepository<GetBoardModel> _getBoardRepo;

		public GetBoardByIdRequestFactory(ILogger<GetBoardByIdRequest> logger,
			IVerbositySetter verbositySetter,
			IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> entityLogger,
			IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> entityWriter,
			IGetEntityRepository<GetBoardModel> getBoardRepo)
		{
			_logger = logger;
			_verbositySetter = verbositySetter;
			_entityLogger = entityLogger;
			_entityWriter = entityWriter;
			_getBoardRepo = getBoardRepo;
		}

		public GetBoardByIdRequest Create(
			BoardInclusionConfiguration boardInclusionConfiguration,
			bool verbosity, bool print,
			string fileLocation,
			int id)
		{
			return new GetBoardByIdRequest(
				_logger,
				_verbositySetter,
				_entityLogger,
				_entityWriter,
				_getBoardRepo,
				boardInclusionConfiguration,
				verbosity,
				print,
				fileLocation,
				id);
		}
	}
}
