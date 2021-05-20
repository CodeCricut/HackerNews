﻿using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models.Users;
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

		public PostVerbProcessor(
			ISignInManager signInManager,
			IEntityApiClient<TPostModel, TGetModel> entityApiClient,
			IEntityLogger<TGetModel> entityLogger)
		{
			_signInManager = signInManager;
			_entityApiClient = entityApiClient;
			_entityLogger = entityLogger;
		}

		public async Task ProcessGetVerbOptionsAsync(TOptions options)
		{
			// TODO: handle bad requests
			LoginModel loginModel = new LoginModel() { UserName = options.Username, Password = options.Password };
			await _signInManager.SignInAsync(loginModel);

			TPostModel postModel = ConstructPostModel(options);
			TGetModel getModel = await _entityApiClient.PostAsync(postModel);
			_entityLogger.LogEntity(getModel);
		}

		public abstract TPostModel ConstructPostModel(TOptions options);
	}
}
