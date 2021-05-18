using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Services
{
	public class BoardLogger : IEntityLogger<GetBoardModel>
	{
		private readonly ILogger<BoardLogger> _logger;

		public BoardLogger(ILogger<BoardLogger> logger)
		{
			_logger = logger;
		}

		public void LogEntity(GetBoardModel board)
		{
			// TODO
			string printString = $"BOARD {board.Id}: Title={board.Title}; Description={board.Description}; CreateDate{board.CreateDate}";
			_logger.LogInformation(printString);
		}

		public void LogEntityPage(PaginatedList<GetBoardModel> boardPage)
		{
			// TODO:
			_logger.LogInformation(boardPage.PageSize.ToString());
		}
	}
}
