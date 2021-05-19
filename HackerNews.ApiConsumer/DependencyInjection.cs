using HackerNews.ApiConsumer.Account;
using HackerNews.ApiConsumer.Core;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.ApiConsumer.Images;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.ApiConsumer
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApiConsumer(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddHttpClient();

			string baseUrl = configuration.GetValue<string>("baseUrl");
			services.AddHttpClient<IApiClient, ApiClient>(config =>
			{
				config.BaseAddress = new System.Uri(baseUrl);
			});

			services.AddSingleton<IEntityApiClient<PostArticleModel, GetArticleModel>, ArticleApiClient>();
			services.AddSingleton<IArticleApiClient, ArticleApiClient>();

			services.AddSingleton<IEntityApiClient<PostBoardModel, GetBoardModel>, BoardApiClient>();
			services.AddSingleton<IBoardApiClient, BoardApiClient>();

			services.AddSingleton<IEntityApiClient<PostCommentModel, GetCommentModel>, CommentApiClient>();
			services.AddSingleton<ICommentApiClient, CommentApiClient>();

			services.AddSingleton<IEntityApiClient<RegisterUserModel, GetPublicUserModel>, UserApiClient>();
			services.AddSingleton<IUserApiClient, UserApiClient>();

			services.AddSingleton<IImageApiClient, ImageApiClient>();

			services.AddSingleton<IRegistrationApiClient, RegistrationApiClient>();

			services.AddSingleton<IPrivateUserApiClient, PrivateUserApiClient>();

			services.AddSingleton<ISignInManager, ApiConsumerSignInManager>();

			return services;
		}
	}
}
