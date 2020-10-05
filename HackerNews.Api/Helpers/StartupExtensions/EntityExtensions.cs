using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Api.Helpers.EntityServices.Base;
using HackerNews.Api.Helpers.EntityServices.Base.ArticleServices;
using HackerNews.Api.Helpers.EntityServices.Base.BoardServices;
using HackerNews.Api.Helpers.EntityServices.Base.CommentServices;
using HackerNews.Api.Helpers.EntityServices.Base.UserServices;
using HackerNews.Api.Helpers.EntityServices.Interfaces;
using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Auth;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
using HackerNews.EF;
using HackerNews.EF.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.Api.Helpers.StartupExtensions
{
	public static class EntityExtensions
	{
		public static IServiceCollection AddEntityRepositories(this IServiceCollection services)
		{
			return services
					.AddScoped<IEntityRepository<Article>, ArticleRepository>()
					.AddScoped<IEntityRepository<Comment>, CommentRepository>()
					.AddScoped<IEntityRepository<User>, UserRepository>()
					.AddScoped<IEntityRepository<Board>, BoardRepository>();
		}

		public static IServiceCollection AddEntityServices(this IServiceCollection services)
		{
			return services
				.AddScoped<IBoardUserManagementService, BoardUserManagmentService>()

				.AddScoped<IModifyEntityService<Article, PostArticleModel, GetArticleModel>, ModifyArticleService>()
				.AddScoped<IModifyEntityService<Comment, PostCommentModel, GetCommentModel>, ModifyCommentService>()
				.AddScoped<IModifyEntityService<Board, PostBoardModel, GetBoardModel>, ModifyBoardService>()
				.AddScoped<IModifyEntityService<User, RegisterUserModel, GetPrivateUserModel>, ModifyPrivateUserService>()

				.AddScoped<IReadEntityService<Article, GetArticleModel>, ReadArticleService>()
				.AddScoped<IReadEntityService<Comment, GetCommentModel>, ReadCommentService>()
				.AddScoped<IReadEntityService<Board, GetBoardModel>, ReadBoardService>()
				.AddScoped<IReadEntityService<User, GetPublicUserModel>, ReadPublicUserService>();
		}

		public static IServiceCollection AddVoteableEntityServices(this IServiceCollection services)
		{
			return services
				.AddScoped<IVoteableEntityService<Article>, VoteArticleService>()
				.AddScoped<IVoteableEntityService<Comment>, VoteCommentService>();
		}

		public static IServiceCollection AddUserServices(this IServiceCollection services)
		{
			return services
				.AddScoped<IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel>, UserAuthService>()
				.AddScoped<IUserSaverService, UserSaverService>()
				.AddScoped<IUserRepository, UserRepository>();
		}

		public static IServiceCollection AddBoardServices(this IServiceCollection services)
		{
			return services.AddScoped<IBoardUserManagementService, BoardUserManagmentService>();
		}
	}
}
