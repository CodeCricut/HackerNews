using HackerNews.CLI.Configuration;
using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace HackerNews.CLI.Requests.GetBoards
{
	public class GetBoardsRequestFactory
	{
		private readonly ILogger<GetBoardsRequest> _logger;
		private readonly IVerbositySetter _verbositySetter;
		private readonly IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> _configBoardLogger;
		private readonly IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> _configBoardWriter;
		private readonly IGetEntityRepository<GetBoardModel> _getBoardRepo;

		public GetBoardsRequestFactory(
			ILogger<GetBoardsRequest> logger,
			IVerbositySetter verbositySetter,
			IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> configBoardLogger,
			IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> configBoardWriter,
			IGetEntityRepository<GetBoardModel> getBoardRepo)
		{
			_logger = logger;
			_verbositySetter = verbositySetter;
			_configBoardLogger = configBoardLogger;
			_configBoardWriter = configBoardWriter;
			_getBoardRepo = getBoardRepo;
		}

		public GetBoardsRequest Create(
			BoardInclusionConfiguration boardInclusionConfig,
			bool verbose,
			bool print,
			string fileLocation,
			IEnumerable<int> ids,
			PagingParams pagingParams)
		{
			return new GetBoardsRequest(
				_logger,
				_verbositySetter,
				_configBoardLogger,
				_configBoardWriter,
				_getBoardRepo,

				boardInclusionConfig,
				verbose,
				print,
				fileLocation,
				ids,
				pagingParams);
		}
	}
}
