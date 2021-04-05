using Hackernews.WPF.ApiClients;
using HackerNews.Domain.Common.Models.Articles;
using MediatR;
using System;

namespace Hackernews.WPF.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		public BoardsListViewModel BoardsListViewModel { get; }
		public ArticlesViewModel ArticlesViewModel { get; }
		public CommentListViewModel CommentListViewModel { get; }
		public UserListViewModel UserListViewModel { get; }

		public NavigationViewModel NavigationViewModel { get; }
		public PrivateUserViewModel UserViewModel { get; }

		public MainWindowViewModel(IApiClient apiClient)
		{
			UserListViewModel = new UserListViewModel(apiClient);
			BoardsListViewModel = new BoardsListViewModel(apiClient);
			ArticlesViewModel = new ArticlesViewModel(apiClient);
			CommentListViewModel = new CommentListViewModel(apiClient);

			NavigationViewModel = new NavigationViewModel(UserListViewModel, BoardsListViewModel, ArticlesViewModel, CommentListViewModel);

			UserViewModel = new PrivateUserViewModel(apiClient);
		}
	}
}
