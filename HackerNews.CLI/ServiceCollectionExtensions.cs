using CommandLine;
using HackerNews.CLI.Domain;
using HackerNews.CLI.HostedServices;
using HackerNews.CLI.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;


namespace HackerNews.CLI
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection RegisterHostedServiceForVerb(this IServiceCollection services, string[] args)
		{
			IVerbAccessor verbAccessor = services.BuildServiceProvider().GetRequiredService<IVerbAccessor>();

			Type[] types = verbAccessor.GetVerbTypes().ToArray();

			Parser.Default.ParseArguments(args, types)
				.WithParsed(o => RegisterHostedService(o, services))
				.WithNotParsed(errors => errors.Output());

			return services;
		}

		//private static Type[] LoadVerbs()
		//{
		//	// Load all verb types using Reflection.
		//	return Assembly.GetExecutingAssembly().GetTypes()
		//		.Where(t => t.GetCustomAttribute<VerbAttribute>() != null).ToArray();
		//}

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

					//case GetArticleByIdOptions opts:
					//	services.AddSingleton(opts);
					//	services.AddHostedService<GetArticleByIdHostedService>();
					//	break;

					//case GetCommentByIdOptions opts:
					//	services.AddSingleton(opts);
					//	services.AddHostedService<GetCommentByIdHostedService>();
					//	break;


					//case GetArticleOptions a:
					//	services.AddSingleton(a);
					//	services.AddHostedService<GetArticlesHostedService>();
					//	break;
					//case GetCommentsOptions c:
					//	services.AddSingleton(c);
					//	services.AddHostedService<GetCommentsHostedService>();
					//	break;
					//case GetPublicUsersOptions u:
					//	services.AddSingleton(u);
					//	services.AddHostedService<GetPublicUsersHostedService>();
					//	break;
					//case PostBoardOptions b:
					//	services.AddSingleton(b);
					//	services.AddHostedService<PostBoardHostedService>();
					//	break;
					//case PostArticleOptions a:
					//	services.AddSingleton(a);
					//	services.AddHostedService<PostArticleHostedService>();
					//	break;
					//case PostCommentOptions c:
					//	services.AddSingleton(c);
					//	services.AddHostedService<PostCommentHostedService>();
					//	break;
			}
		}


	}
}
