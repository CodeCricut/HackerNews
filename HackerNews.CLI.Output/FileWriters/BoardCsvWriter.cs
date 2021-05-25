using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.FileWriters
{
	public class BoardCsvWriter : IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration>
	{
		private readonly IFileWriter _fileWriter;
		private readonly ILogger<BoardCsvWriter> _logger;
		private readonly IEntityInclusionReader<BoardInclusionConfiguration, GetBoardModel> _boardInclusionReader;
		private BoardInclusionConfiguration _inclusionConfig;

		public BoardCsvWriter(IFileWriter writer,
			ILogger<BoardCsvWriter> logger,
			IEntityInclusionReader<BoardInclusionConfiguration, GetBoardModel> boardInclusionReader)
		{
			_fileWriter = writer;
			_logger = logger;
			_boardInclusionReader = boardInclusionReader;
			_inclusionConfig = new BoardInclusionConfiguration();

			_logger.LogTrace("Created " + this.GetType().Name);
		}

		public Task WriteEntityAsync(string fileLoc, GetBoardModel entity)
		{
			_logger.LogDebug($"Attempting to write board to {fileLoc}...");

			List<GetBoardModel> boards = new List<GetBoardModel>();
			boards.Add(entity);
			return WriteEntitiesToFileAsync(fileLoc, boards);
		}

		public void Configure(BoardInclusionConfiguration config)
		{
			_inclusionConfig = config;
		}

		public Task WriteEntityPageAsync(string fileLoc, PaginatedList<GetBoardModel> entityPage)
		{
			_logger.LogDebug($"Attempting to write page with {entityPage.Items.Count} boards to {fileLoc}...");

			return WriteEntitiesToFileAsync(fileLoc, entityPage.Items);
		}

		private async Task WriteEntitiesToFileAsync(string fileLoc, IEnumerable<GetBoardModel> boards)
		{
			List<string> lines = new List<string>();

			lines.Add(GetHeadLine());

			foreach (var board in boards)
				lines.Add(GetBodyLine(board));

			_logger.LogInformation($"Adding {lines.Count} lines to {fileLoc}...");

			try
			{
				await _fileWriter.WriteToFileAsync(fileLoc, lines);
				_logger.LogInformation($"Wrote {lines.Count} lines to {fileLoc}...");
			}
			catch (Exception)
			{
				_logger.LogError($"Error adding {lines.Count} lines to {fileLoc}. Aborting");
				throw;
			}
		}

		private string GetHeadLine()
		{
			var keys = _boardInclusionReader.ReadIncludedKeys(_inclusionConfig);

			StringBuilder sb = new StringBuilder();
			foreach (var key in keys)
			{
				sb.Append($"{key},");
			}

			return sb.ToString();
		}

		private string GetBodyLine(GetBoardModel board)
		{
			var values = _boardInclusionReader.ReadIncludedValues(_inclusionConfig, board);

			StringBuilder sb = new StringBuilder();
			foreach (var value in values)
			{
				sb.Append($"{value},");
			}

			return sb.ToString();
		}
	}
}
