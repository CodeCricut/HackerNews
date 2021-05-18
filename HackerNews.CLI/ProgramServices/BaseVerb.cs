using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.ApiConsumer.Account;

namespace HackerNews.CLI.ProgramServices
{
	public abstract class BaseVerb : IHostedService, IDisposable
	{
		protected IBoardApiClient BoardApiClient { get; }
		protected IArticleApiClient ArticleApiClient { get; }
		protected ICommentApiClient CommentApiClient { get; }
		protected IUserApiClient UserApiClient { get; }
		protected IRegistrationApiClient RegistrationApiClient { get; }
		protected IPrivateUserApiClient PrivateUserApiClient { get; }

		public BaseVerb(IBoardApiClient boardApiClient,
			IArticleApiClient articleApiClient,
			ICommentApiClient commentApiClient,
			IUserApiClient userApiClient,
			
			IRegistrationApiClient registrationApiClient,
			IPrivateUserApiClient privateUserApiClient)
		{
			BoardApiClient = boardApiClient;
			ArticleApiClient = articleApiClient;
			CommentApiClient = commentApiClient;
			UserApiClient = userApiClient;
			RegistrationApiClient = registrationApiClient;
			PrivateUserApiClient = privateUserApiClient;
		}

		public abstract Task StartAsync(CancellationToken cancellationToken);
		public abstract Task StopAsync(CancellationToken cancellationToken);
		public abstract void Dispose();
	}
}
