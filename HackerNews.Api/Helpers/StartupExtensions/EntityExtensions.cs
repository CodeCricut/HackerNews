using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Api.Helpers.EntityServices.Base;
using HackerNews.Api.Helpers.EntityServices.Base.ArticleServices;
using HackerNews.Api.Helpers.EntityServices.Base.UserServices;
using HackerNews.Api.Helpers.EntityServices.Default;
using HackerNews.Domain;
using HackerNews.EF;
using HackerNews.EF.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
			return services.AddScoped<VoteArticleService, DefaultArticleService>()
				.AddScoped<CommentService, DefaultCommentService>()
				.AddScoped<UserService, DefaultUserService>()
				.AddScoped<BoardService, DefaultBoardService>();
		}

		public static IServiceCollection AddVoteableEntityServices(this IServiceCollection services)
		{
			return services.AddScoped<IVoteableEntityService<Article>, DefaultArticleService>()
				.AddScoped<IVoteableEntityService<Comment>, DefaultCommentService>();
		}

		public static IServiceCollection AddUserServices(this IServiceCollection services)
		{
			return services.AddScoped<UserAuthService, DefaultUserAuthService>()
				.AddScoped<UserSaverService, DefaultUserSaverService>()
				.AddScoped<IUserRepository, UserRepository>();
		}

		public static IServiceCollection AddBoardServices(this IServiceCollection services)
		{
			return services.AddScoped<BoardUserManagmentService, DefaultBoardUserManagementService>();
		}
	}
}
