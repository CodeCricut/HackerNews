using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.FileWriters
{
	public class BoardCsvWriter : IEntityWriter<GetBoardModel>
	{
		private readonly IFileWriter _fileWriter;
		private readonly ILogger<BoardCsvWriter> _logger;
		private BoardInclusionConfiguration _inclusionConfig;

		public BoardCsvWriter(IFileWriter writer, ILogger<BoardCsvWriter> logger)
		{
			_fileWriter = writer;
			_logger = logger;
			_inclusionConfig = new BoardInclusionConfiguration();
		}

		public Task WriteEntityAsync(string fileLoc, GetBoardModel entity)
		{
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
			StringBuilder head = new StringBuilder();
			if (_inclusionConfig.IncludeId) head.Append("ID,");
			if (_inclusionConfig.IncludeTitle) head.Append("TITLE,");
			if (_inclusionConfig.IncludeDescription) head.Append("DESCRIPTION,");
			if (_inclusionConfig.IncludeCreateDate) head.Append("CREATE DATE,");
			if (_inclusionConfig.IncludeCreatorId) head.Append("CREATOR ID,");
			if (_inclusionConfig.IncludeModeratorIds) head.Append("MODERATOR IDS");
			if (_inclusionConfig.IncludeSubscriberIds) head.Append("SUBSCRIBER IDS");
			if (_inclusionConfig.IncludeArticleIds) head.Append("ARTICLE IDS");
			if (_inclusionConfig.IncludeDeleted) head.Append("DELETED");
			if (_inclusionConfig.IncludeBoardImageId) head.Append("BOARD IMAGE ID");

			return head.ToString();
		}

		private string GetBodyLine(GetBoardModel board)
		{
			char delimiter = ',';
			StringBuilder body = new StringBuilder();
			if (_inclusionConfig.IncludeId) body.Append($"{board.Id},");
			if (_inclusionConfig.IncludeTitle) body.Append($"{Quote(board.Title)},");
			if (_inclusionConfig.IncludeDescription) body.Append($"{Quote(board.Description)},");
			if (_inclusionConfig.IncludeCreateDate) body.Append($"{Quote(board.CreateDate.ToString())},");
			if (_inclusionConfig.IncludeCreatorId) body.Append($"{board.CreatorId},");
			if (_inclusionConfig.IncludeModeratorIds) body.Append($"{Quote(board.ModeratorIds.ToDelimitedList(delimiter))},");
			if (_inclusionConfig.IncludeSubscriberIds) body.Append($"{Quote(board.SubscriberIds.ToDelimitedList(delimiter))},");
			if (_inclusionConfig.IncludeArticleIds) body.Append($"{Quote(board.ArticleIds.ToDelimitedList(delimiter))},");
			if (_inclusionConfig.IncludeDeleted) body.Append($"{board.Deleted},");
			if (_inclusionConfig.IncludeBoardImageId) body.Append($"{board.BoardImageId},");

			return body.ToString();
		}

		private string Quote(string s)
		{
			return $"\"{s}\"";
		}
	}
}
