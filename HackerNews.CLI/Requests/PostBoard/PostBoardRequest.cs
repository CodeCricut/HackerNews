using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Configuration;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Request.Core;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Requests.PostBoard
{
	public class PostBoardRequest : IRequest
	{
		private readonly ILogger<PostBoardRequest> _logger;
		private readonly IVerbositySetter _verbositySetter;
		private readonly ISignInManager _signInManager;
		private readonly IEntityApiClient<PostBoardModel, GetBoardModel> _boardApiClient;
		private readonly IEntityLogger<GetBoardModel> _entityLogger;
		private readonly IEntityWriter<GetBoardModel> _entityWriter;
		private readonly bool _verbose;
		private readonly LoginModel _loginModel;
		private readonly bool _print;
		private readonly string _fileLocation;
		private readonly PostBoardModel _postBoardModel;

		public PostBoardRequest(ILogger<PostBoardRequest> logger,
			IVerbositySetter verbositySetter,
			ISignInManager signInManager,
			IEntityApiClient<PostBoardModel, GetBoardModel> boardApiClient,
			IEntityLogger<GetBoardModel> entityLogger,
			IEntityWriter<GetBoardModel> entityWriter,

			bool verbose,
			LoginModel loginModel,
			bool print,
			string fileLocation,
			PostBoardModel postBoardModel
			)
		{
			_logger = logger;
			_verbositySetter = verbositySetter;
			_signInManager = signInManager;
			_boardApiClient = boardApiClient;
			_entityLogger = entityLogger;
			_entityWriter = entityWriter;
			_verbose = verbose;
			_loginModel = loginModel;
			_print = print;
			_fileLocation = fileLocation;
			_postBoardModel = postBoardModel;

		}

		public async Task ExecuteAsync()
		{
			_verbositySetter.SetVerbository(_verbose);

			bool signedIn = await TrySignInAsync();
			if (!signedIn) return;

			var board = await PostBoardAsync();
			if (board == null) return;

			if (_print)
				_entityLogger.LogEntity(board);

			if (!string.IsNullOrEmpty(_fileLocation))
				await _entityWriter.WriteEntityAsync(_fileLocation, board);
		}

		public Task CancelAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		/// <returns>Successful.</returns>
		private async Task<bool> TrySignInAsync()
		{
			try
			{
				_logger.LogInformation("Attempting to log in...");
				await _signInManager.SignInAsync(_loginModel);
				return true;
			}
			catch (System.Exception e)
			{
				_logger.LogWarning(e.Message);
				_logger.LogWarning("Could not log in successfully. Aborting.");
				return false;
			}
		}

		private async Task<GetBoardModel> PostBoardAsync()
		{
			try
			{
				_logger.LogInformation("Attempting to post board...");
				GetBoardModel postedBoard = await _boardApiClient.PostAsync(_postBoardModel);
				_logger.LogInformation("Successfully posted entity.");
				return postedBoard;
			}
			catch (System.Exception e)
			{
				_logger.LogError("Error when posting entity. Entity may or may not have been posted to the database.");
				return null;
			}
		}
	}
}
