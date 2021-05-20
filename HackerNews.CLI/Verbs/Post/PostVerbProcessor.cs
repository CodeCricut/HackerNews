using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Configuration;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.Post
{
	public interface IPostVerbProcessor<TPostModel, TGetModel, TOptions>
		where TOptions : IPostEntityOptions
	{
		TPostModel ConstructPostModel(TOptions options);
		Task ProcessGetVerbOptionsAsync(TOptions options);
	}

	public abstract class PostVerbProcessor<TPostModel, TGetModel, TOptions> 
		: IPostVerbProcessor<TPostModel, TGetModel, TOptions>
		where TOptions : IPostEntityOptions
	{
		private readonly ISignInManager _signInManager;
		private readonly IEntityApiClient<TPostModel, TGetModel> _entityApiClient;
		private readonly IEntityLogger<TGetModel> _entityLogger;
		private readonly ILogger<PostVerbProcessor<TPostModel, TGetModel, TOptions>> _logger;
		private readonly IVerbositySetter _verbositySetter;

		public PostVerbProcessor(
			ISignInManager signInManager,
			IEntityApiClient<TPostModel, TGetModel> entityApiClient,
			IEntityLogger<TGetModel> entityLogger,
			ILogger<PostVerbProcessor<TPostModel, TGetModel, TOptions>> logger,
			IVerbositySetter verbositySetter)
		{
			_signInManager = signInManager;
			_entityApiClient = entityApiClient;
			_entityLogger = entityLogger;
			_logger = logger;
			_verbositySetter = verbositySetter;
		}

		public async Task ProcessGetVerbOptionsAsync(TOptions options)
		{
			if (options.Verbose)
				_verbositySetter.SetVerbository(true);

			bool signedIn = await TrySignInAsync(options);
			if (!signedIn) return;
			await PostEntity(options);

		}

		private async Task PostEntity(TOptions options)
		{
			try
			{
				_logger.LogInformation("Attempting to post entity...");
				TPostModel postModel = ConstructPostModel(options);
				TGetModel getModel = await _entityApiClient.PostAsync(postModel);
				_logger.LogInformation("Successfully posted entity.");
				_entityLogger.LogEntity(getModel);
			}
			catch (System.Exception e)
			{
				_logger.LogError("Error when posting entity. Entity may or may not have been posted to the database.");
			}
		}

		/// <returns>Successful.</returns>
		private async Task<bool> TrySignInAsync(TOptions options)
		{
			try
			{
				_logger.LogInformation("Attempting to log in...");
				LoginModel loginModel = new LoginModel() { UserName = options.Username, Password = options.Password };
				await _signInManager.SignInAsync(loginModel);
				return true;
			}
			catch (System.Exception e)
			{
				_logger.LogWarning("Could not log in successfully. Aborting.");
				return false;
			}
		}

		public abstract TPostModel ConstructPostModel(TOptions options);
	}
}
