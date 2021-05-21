﻿using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.CLI.Loggers
{
	public class BoardLogger : IEntityLogger<GetBoardModel>
	{
		private readonly ILogger<BoardLogger> _logger;
		private readonly IEntityReader<GetBoardModel> _boardReader;

		public BoardLogger(ILogger<BoardLogger> logger,
			IEntityReader<GetBoardModel> boardReader)
		{
			_logger = logger;
			_boardReader = boardReader;

			logger.LogTrace("Created " + this.GetType().Name);
		}

		public void LogEntity(GetBoardModel board)
		{
			_logger.LogDebug("Logging board.");

			LogBoard(board);
		}

		public void LogEntityPage(PaginatedList<GetBoardModel> boardPage)
		{
			_logger.LogDebug("Logging board page.");

			_logger.LogInformation($"BOARD PAGE {boardPage.PageIndex}/{boardPage.TotalPages}; Showing {boardPage.PageSize} / {boardPage.TotalCount} Boards");
			foreach (var board in boardPage.Items)
			{
				_logger.LogTrace($"Logging board with ID={board.Id} in board page.");
				LogBoard(board);
			}
		}


		private void LogBoard(GetBoardModel board)
		{
			Dictionary<string, string> boardDict = _boardReader.ReadAllKeyValues(board);

			_logger.LogInformation("---------------------");
			foreach (var kvp in boardDict)
				_logger.LogInformation($"\t{kvp.Key}={kvp.Value}");
			_logger.LogInformation("---------------------");
		}
	}
}
