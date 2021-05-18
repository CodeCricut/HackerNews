using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.FileWriters
{
	public interface IBoardFileWriter
	{
		// TODO: 
		// void Configure(BoardInclusionConfiguration printerConfig);
		// void Configure(Action<BoardInclusionConfiguration> printerConfigFunc);
		//Task WriteBoardToFile(IEnumerable<GetBoardModel> boards, string fileLoc);
	}

	public class BoardCsvWriter : IEntityWriter<GetBoardModel>
	{
		private readonly IFileWriter _fileWriter;
		private readonly ILogger<BoardCsvWriter> _logger;

		public BoardCsvWriter(IFileWriter writer, ILogger<BoardCsvWriter> logger)
		{
			_fileWriter = writer;
			_logger = logger;
		}

		public Task WriteEntityAsync(string fileLoc, GetBoardModel entity)
		{
			List<GetBoardModel> boards = new List<GetBoardModel>();
			boards.Add(entity);
			return WriteEntitiesToFileAsync(fileLoc, boards);
		}

		public Task WriteEntityPageAsync(string fileLoc, PaginatedList<GetBoardModel> entityPage)
		{
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
			// TODO: use inclusion config
			return "TITLE,DESCRIPTION";
		}

		private string GetBodyLine(GetBoardModel board)
		{
			// TODO: use inclusion config
			return $"{board.Title}, {board.Description}";
		}
	}
}
