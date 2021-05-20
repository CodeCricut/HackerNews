using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.CLI.Loggers
{
	public class BoardLogger : IConfigurableEntityLogger<GetBoardModel, BoardInclusionConfiguration>
	{
		private readonly ILogger<BoardLogger> _logger;
		private readonly IEntityInclusionReader<BoardInclusionConfiguration, GetBoardModel> _boardInclusionReader;
		private BoardInclusionConfiguration _inclusionConfig;

		public BoardLogger(ILogger<BoardLogger> logger,
			IEntityInclusionReader<BoardInclusionConfiguration, GetBoardModel> boardInclusionReader)
		{
			_logger = logger;
			_boardInclusionReader = boardInclusionReader;
			_inclusionConfig = new BoardInclusionConfiguration();

			logger.LogTrace("Created " + this.GetType().Name);
		}

		public void Configure(BoardInclusionConfiguration config)
		{
			_logger.LogTrace("Configuring " + this.GetType().Name);
			_inclusionConfig = config;
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
			Dictionary<string, string> boardDict = _boardInclusionReader.ReadIncludedKeyValues(_inclusionConfig, board);

			_logger.LogInformation("---------------------");
			foreach (var kvp in boardDict)
				_logger.LogInformation($"\t{kvp.Key}={kvp.Value}");
			_logger.LogInformation("---------------------");
		}
	}
}
