using HackerNews.CLI.Configuration;
using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.GetBoardById
{
	public class GetBoardByIdRequest
	{
		private readonly ILogger<GetBoardByIdRequest> _logger;
		private readonly IVerbositySetter _verbositySetter;
		private readonly IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> _entityLogger;
		private readonly IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> _entityWriter;
		private readonly IGetEntityRepository<GetBoardModel> _getBoardRepo;

		private readonly BoardInclusionConfiguration _boardInclusionConfiguration;
		private readonly bool _verbose;
		private readonly bool _print;
		private readonly string _fileLocation;
		private readonly int _id;

		public GetBoardByIdRequest(
			ILogger<GetBoardByIdRequest> logger,
			IVerbositySetter verbositySetter,
			IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> entityLogger,
			IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> entityWriter,
			IGetEntityRepository<GetBoardModel> getBoardRepo,
			
			BoardInclusionConfiguration boardInclusionConfiguration,
			bool verbose, 
			bool print,
			string fileLocation,
			int id)
		{
			_logger = logger;
			_verbositySetter = verbositySetter;
			_entityLogger = entityLogger;
			_entityWriter = entityWriter;
			_getBoardRepo = getBoardRepo;
			_boardInclusionConfiguration = boardInclusionConfiguration;
			_verbose = verbose;
			_print = print;
			_fileLocation = fileLocation;
			_id = id;
		}

		public async Task ExecuteAsync()
		{
			_verbositySetter.SetVerbository(_verbose);

			GetBoardModel board = await _getBoardRepo.GetByIdAsync(_id);
			if (board == null)
			{
				_logger.LogWarning($"Could not find a board with the ID of {_id}. Aborting request...");
				return;
			}

			if (_print)
			{
				_entityLogger.Configure(_boardInclusionConfiguration);
				_entityLogger.LogEntity(board);
			}
			
			if (!string.IsNullOrEmpty(_fileLocation))
			{
				_entityWriter.Configure(_boardInclusionConfiguration);
				await _entityWriter.WriteEntityAsync(_fileLocation, board);
			}
		}

		public Task CancelAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
