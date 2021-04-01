﻿using HackerNews.Domain.Common.Models.Articles;
using MediatR;

namespace Hackernews.WPF.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		public BoardsListViewModel BoardsListViewModel { get; }
		public ArticlesViewModel ArticlesViewModel { get; }

		public NavigationViewModel NavigationViewModel { get; }

		public MainWindowViewModel(IMediator mediator)
		{
			BoardsListViewModel = new BoardsListViewModel(mediator);
			ArticlesViewModel = new ArticlesViewModel(mediator);

			NavigationViewModel = new NavigationViewModel(BoardsListViewModel, ArticlesViewModel);
		}
	}
}
