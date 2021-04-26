using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using HackerNews.ApiConsumer.Core;
using HackerNews.ApiConsumer.EntityClients;

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

			return services;
		}
	}
}
