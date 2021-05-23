using HackerNews.CLI.Request.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.Request.TestCases
{
	public class GetBoardByIdOptions
	{
		public int BoardId { get; }

		public GetBoardByIdOptions(int boardId)
		{
			BoardId = boardId;
		}
	}
	public class GetBoardByIdRequestHandler : IRequestHandler<GetBoardByIdOptions>
	{
		private readonly ILogger<GetBoardByIdRequestHandler> _logger;

		public GetBoardByIdRequestHandler(ILogger<GetBoardByIdRequestHandler> logger)
		{
			_logger = logger;
		}

		public Task HandleAsync(GetBoardByIdOptions options)
		{
			_logger.LogInformation($"Getting board with id {options.BoardId}.");
			return Task.CompletedTask;
		}
	}
}
