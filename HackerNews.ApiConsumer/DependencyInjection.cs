using HackerNews.ApiConsumer.Account;
using HackerNews.ApiConsumer.Core;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.ApiConsumer.Images;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.ApiConsumer
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApiConsumer(this IServiceCollection services)
		{
			services.AddHttpClient();

			services.AddHttpClient<IApiClient, ApiClient>(config =>
			{
				// TODO: put in config file
				string baseUrl = "https://localhost:44300/api/";
				config.BaseAddress = new System.Uri(baseUrl);
			});

			services.AddSingleton<IArticleApiClient, ArticleApiClient>();
			services.AddSingleton<IBoardApiClient, BoardApiClient>();
			services.AddSingleton<ICommentApiClient, CommentApiClient>();
			services.AddSingleton<IUserApiClient, UserApiClient>();

			services.AddSingleton<IImageApiClient, ImageApiClient>();

			services.AddSingleton<IRegistrationApiClient, RegistrationApiClient>();

			services.AddSingleton<IPrivateUserApiClient, PrivateUserApiClient>();

			services.AddSingleton<ISignInManager, ApiConsumerSignInManager>();

			return services;
		}
	}
}
