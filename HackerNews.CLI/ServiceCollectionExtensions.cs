using CommandLine;
using HackerNews.CLI.Domain;
using HackerNews.CLI.Domain.Verbs;
using HackerNews.CLI.HostedServices;
using HackerNews.CLI.Options;
using HackerNews.CLI.Options.Verbs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;


namespace HackerNews.CLI
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection RegisterHostedServices(this IServiceCollection services, string[] args)
		{
			IVerbAccessor verbAccessor = services.BuildServiceProvider().GetRequiredService<IVerbAccessor>();

			Type[] types = verbAccessor.GetVerbTypes().ToArray();

			Parser.Default.ParseArguments(args, types)
				.WithParsed(o => RegisterHostedService(o, services))
				.WithNotParsed(errors => errors.Output());

			return services;
		}

		private static void RegisterHostedService(object verb, IServiceCollection services)
		{
			switch (verb)
			{
				case GetBoardByIdOptions opts:
					services.AddSingleton(opts);
					services.AddHostedService<GetBoardByIdHostedService>();
					break;
				case GetBoardsOptions opts:
					services.AddSingleton(opts);
					services.AddHostedService<GetBoardsHostedService>();
					break;
				case PostBoardOptions opts:
					services.AddSingleton(opts);
					services.AddHostedService<PostBoardHostedService>();
					break;

				case GetArticleByIdOptions opts:
					services.AddSingleton(opts);
					services.AddHostedService<GetArticleByIdHostedService>();
					break;
				case GetArticlesOptions opts:
					services.AddSingleton(opts);
					services.AddHostedService<GetArticlesHostedService>();
					break;
				case PostArticleOptions opts:
					services.AddSingleton(opts);
					services.AddHostedService<PostArticleHostedService>();
					break;

				case GetCommentByIdOptions opts:
					services.AddSingleton(opts);
					services.AddHostedService<GetCommentByIdHostedService>();
					break;
				case GetCommentsOptions opts:
					services.AddSingleton(opts);
					services.AddHostedService<GetCommentsHostedService>();
					break;
				case PostCommentOptions opts:
					services.AddSingleton(opts);
					services.AddHostedService<PostCommentHostedService>();
					break;

				case GetPublicUserByIdOptions opts:
					services.AddSingleton(opts);
					services.AddHostedService<GetPublicUserByIdHostedService>();
					break;
				case GetPublicUsersOptions opts:
					services.AddSingleton(opts);
					services.AddHostedService<GetPublicUsersHostedService>();
					break;
			}
		}
	}
}
