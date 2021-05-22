using HackerNews.ApiConsumer.Core;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.CLI.Configuration;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Logging;

namespace HackerNews.CLI.Requests.PostBoard
{
	public class PostBoardRequestFactory
	{
		private readonly ILogger<PostBoardRequest> _logger;
		private readonly IVerbositySetter _verbositySetter;
		private readonly ISignInManager _signInManager;
		private readonly IBoardApiClient _boardApiClient;
		private readonly IEntityLogger<GetBoardModel> _boardLogger;
		private readonly IEntityWriter<GetBoardModel> _boardWriter;

		public PostBoardRequestFactory(
			ILogger<PostBoardRequest> logger,
			IVerbositySetter verbositySetter,
			ISignInManager signInManager,
			IBoardApiClient boardApiClient,
			IEntityLogger<GetBoardModel> boardLogger,
			IEntityWriter<GetBoardModel> boardWriter)
		{
			_logger = logger;
			_verbositySetter = verbositySetter;
			_signInManager = signInManager;
			_boardApiClient = boardApiClient;
			_boardLogger = boardLogger;
			_boardWriter = boardWriter;
		}

		public PostBoardRequest Create(
			bool verbose,
			LoginModel loginModel,
			bool print,
			string fileLocation,
			PostBoardModel postBoardModel)
		{
			return new PostBoardRequest(
				_logger,
				_verbositySetter,
				_signInManager,
				_boardApiClient,
				_boardLogger,
				_boardWriter,

				verbose,
				loginModel,
				print,
				fileLocation,
				postBoardModel
				);
		}
	}
}
