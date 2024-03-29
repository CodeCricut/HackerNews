﻿using Hackernews.WPF.Factories.Commands;
using Hackernews.WPF.Factories.ViewModels;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.WPF.Core.ViewModel;
using HackerNews.WPF.MessageBus.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Hackernews.WPF.Configuration
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddWPF(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddLogging();

			services.AddTransient<MainWindow>();
			services.AddTransient<LoginWindow>();
			services.AddTransient<EntityHomeWindow>();
			services.AddTransient<BoardCreationWindow>();
			services.AddTransient<ArticleCreationWindow>();

			// Register all vms
			services.Scan(scan =>
				scan.FromCallingAssembly()
					.AddClasses(c => c.AssignableTo<BaseViewModel>()) // 1. Find the concrete vms
					.UsingRegistrationStrategy(RegistrationStrategy.Skip) // 2. Define how to handle duplicates
					  .AsSelf() // 2. Specify which services they are registered as
					  .WithTransientLifetime());  // 3. Set the lifetime for the services

			services.AddSingleton<LoginWindowViewModel>();

			services.AddSingleton<PrivateUserViewModel>();


			services.AddSingleton<IEventAggregator, EventAggregator>();

			services.AddSingleton<IViewManager, ViewManager>();

			services.AddSingleton<AppMessageHandler>();

			services.AddTransient<IMainWindowVmFactory, MainWindowVmFactory>();

			services.AddTransient<ILoginWindowVmFactory, LoginWindowVmFactory>();

			services.AddTransient<IPublicUserViewModelFactory, PublicUserViewModelFactory>();
			services.AddTransient<ILoadUsersCommandFactory, LoadUsersCommandFactory>();
			services.AddTransient<IUserListViewModelFactory, UserListViewModelFactory>();

			services.AddTransient<IBoardViewModelFactory, BoardViewModelFactory>();
			services.AddTransient<ILoadBoardsCommandFactory, LoadBoardsCommandFactory>();
			services.AddTransient<IBoardListViewModelFactory, BoardListViewModelFactory>();
			services.AddTransient<IBoardHomeViewModelFactory, BoardHomeViewModelFactory>();
			services.AddTransient<IBoardCreationViewModelFactory, BoardCreationViewModelFactory>();

			services.AddTransient<IArticleViewModelFactory, ArticleViewModelFactory>();
			services.AddTransient<ILoadArticlesCommandFactory, LoadArticlesCommandFactory>();
			services.AddTransient<IArticleListViewModelFactory, ArticleListViewModelFactory>();
			services.AddTransient<IArticleHomeViewModelFactory, ArticleHomeViewModelFactory>();
			services.AddTransient<IArticleCreationViewModelFactory, ArticleCreationViewModelFactory>();

			services.AddTransient<ICommentViewModelFactory, CommentViewModelFactory>();
			services.AddTransient<ILoadCommentsCommandFactory, LoadCommentsCommandFactory>();
			services.AddTransient<ICommentListViewModelFactory, CommentListViewModelFactory>();

			services.AddTransient<IEntityHomeViewModelFactory, EntityHomeViewModelFactory>();

			return services;
		}
	}
}
