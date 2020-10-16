using CleanEntityArchitecture.EntityModelServices;
using CleanEntityArchitecture.Repository;
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
using HackerNews.EF.Repositories.Articles;
using HackerNews.EF.Repositories.Boards;
using HackerNews.EF.Repositories.Comments;
using HackerNews.EF.Repositories.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace HackerNews.Api.Helpers.StartupExtensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddEntityRepositories(this IServiceCollection services)
		{
			//var efAssembly = typeof(HackerNewsContext).Assembly;

			//var types = Assembly.GetExecutingAssembly().GetTypes().Concat(efAssembly.GetTypes());

			//Type readRepoInterfaceType = typeof(IReadEntityRepository<>);
			//Type writeRepoInterfaceType = typeof(IWriteEntityRepository<>);


			//var readRepos = types
			//	.Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition().Equals(readRepoInterfaceType))).ToList();
			//var writeRepos = types
			//	.Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition().Equals(writeRepoInterfaceType))).ToList();

			//foreach(var readrepo in readRepos)
			//{
			//	var baseType = readrepo.BaseType;
			//	var args = baseType.GetGenericArguments();

			//	Type firstArg = args.First();

			//	services.AddScoped<IReadEntityRepository<firstArg>
			//}



			return services
				.AddScoped<IReadEntityRepository<Article>, ReadArticleRepository>()
				.AddScoped<IReadEntityRepository<Comment>, ReadCommentRepository>()
				.AddScoped<IReadEntityRepository<Board>, ReadBoardRepository>()
				.AddScoped<IReadEntityRepository<User>, ReadUserRepository>()

				.AddScoped<IWriteEntityRepository<Article>, WriteArticleRepository>()
				.AddScoped<IWriteEntityRepository<Comment>, WriteCommentRepository>()
				.AddScoped<IWriteEntityRepository<Board>, WriteBoardRepository>()
				.AddScoped<IWriteEntityRepository<User>, WriteUserRepository>();
		}

		public static IServiceCollection AddEntityServices(this IServiceCollection services)
		{
			return services
				.AddScoped<IReadEntityService<Article, GetArticleModel>, ReadArticleService>()
				.AddScoped<IReadEntityService<Comment, GetCommentModel>, ReadCommentService>()
				.AddScoped<IReadEntityService<Board, GetBoardModel>, ReadBoardService>()
				.AddScoped<IReadEntityService<User, GetPublicUserModel>, ReadPublicUserService>()

				.AddScoped<IWriteEntityService<Article, PostArticleModel>, WriteArticleService>()
				.AddScoped<IWriteEntityService<Comment, PostCommentModel>, WriteCommentService>()
				.AddScoped<IWriteEntityService<Board, PostBoardModel>, WriteBoardService>()
				.AddScoped<IWriteEntityService<User, RegisterUserModel>, WritePrivateUserService>()

				.AddScoped<IBoardUserManagementService, BoardUserManagmentService>();
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
				.AddScoped<IAuthenticatableEntityService<User, LoginModel, GetPrivateUserModel>, AuthenticateUserService>()
				.AddScoped<IUserSaverService, UserSaverService>()
				.AddScoped<IUserLoginRepository, UserLoginRepository>();
		}

		public static IServiceCollection AddBoardServices(this IServiceCollection services)
		{
			return services.AddScoped<IBoardUserManagementService, BoardUserManagmentService>();
		}
	}
}
