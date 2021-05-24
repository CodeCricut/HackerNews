using HackerNews.CLI.Configuration;
using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Request.Core;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Requests.GetBoards
{
	public class GetBoardsRequest : IRequest
	{
		private readonly ILogger<GetBoardsRequest> _logger;
		private readonly IVerbositySetter _verbositySetter;
		private readonly IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> _configBoardLogger;
		private readonly IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> _configBoardWriter;
		private readonly IEntityFinder<GetBoardModel> _getBoardRepo;
		private readonly BoardInclusionConfiguration _boardInclusionConfiguration;
		private readonly bool _verbose;
		private readonly bool _print;
		private readonly string _fileLocation;
		private readonly IEnumerable<int> _ids;
		private readonly PagingParams _pagingParams;

		public GetBoardsRequest(
			ILogger<GetBoardsRequest> logger,
			IVerbositySetter verbositySetter,
			IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration> configBoardLogger,
			IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> configBoardWriter,
			IEntityFinder<GetBoardModel> getBoardRepo,

			BoardInclusionConfiguration boardInclusionConfiguration,
			bool verbose,
			bool print,
			string fileLocation,
			IEnumerable<int> ids,
			PagingParams pagingParams)
		{
			_logger = logger;
			_verbositySetter = verbositySetter;
			_configBoardLogger = configBoardLogger;
			_configBoardWriter = configBoardWriter;
			_getBoardRepo = getBoardRepo;
			_boardInclusionConfiguration = boardInclusionConfiguration;
			_verbose = verbose;
			_print = print;
			_fileLocation = fileLocation;
			_ids = ids;
			_pagingParams = pagingParams;
		}

		public async Task ExecuteAsync()
		{
			_verbositySetter.SetVerbository(_verbose);

			var boardPage = await _getBoardRepo.GetByIdsAsync(_ids, _pagingParams);

			if (_print)
			{
				_configBoardLogger.Configure(_boardInclusionConfiguration);
				_configBoardLogger.LogEntityPage(boardPage);
			}

			if (!string.IsNullOrEmpty(_fileLocation))
			{
				_configBoardWriter.Configure(_boardInclusionConfiguration);
				await _configBoardWriter.WriteEntityPageAsync(_fileLocation, boardPage);
			}
		}

		public Task CancelAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
