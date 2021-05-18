using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.Verbs.Post
{
	public interface IPostVerbProcessor<TPostModel, TGetModel>
	{
		Task ProcessGetVerbOptionsAsync(PostVerbOptions options);
	}

	public abstract class PostVerbProcessor<TPostModel, TGetModel> : IPostVerbProcessor<TPostModel, TGetModel>
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

		public async Task ProcessGetVerbOptionsAsync(PostVerbOptions options)
		{
			LoginModel loginModel = new LoginModel() { UserName = options.Username, Password = options.Password };
			await _signInManager.SignInAsync(loginModel);

			TPostModel postModel = ConstructPostModel(options);
			TGetModel getModel = await _entityApiClient.PostAsync(postModel);
			_entityLogger.LogEntity(getModel);
		}

		protected abstract TPostModel ConstructPostModel(PostVerbOptions options);
	}
}
