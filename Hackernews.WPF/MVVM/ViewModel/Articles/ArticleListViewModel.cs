﻿using Hackernews.WPF.Core;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.Model;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class ArticleListViewModel : BaseViewModel, IPageNavigatorViewModel
	{
		public PagingParams PagingParams = new PagingParams();

		public ObservableCollection<ArticleViewModel> Articles { get; private set; } = new ObservableCollection<ArticleViewModel>();

		public PaginatedListViewModel<GetArticleModel> ArticlePageVM { get; }

		public BaseCommand LoadCommand { get; set; }
		public AsyncDelegateCommand PrevPageCommand { get; }
		public AsyncDelegateCommand NextPageCommand { get; }

		public int CurrentPage
		{
			get => ArticlePageVM.CurrentPageNumber;
		}
		public int TotalPages
		{
			get => ArticlePageVM.TotalPages;
		}

		public ArticleListViewModel(CreateBaseCommand<ArticleListViewModel> createLoadCommand)
		{
			ArticlePageVM = new PaginatedListViewModel<GetArticleModel>();

			LoadCommand = createLoadCommand(this);
			NextPageCommand = new AsyncDelegateCommand(NextPageAsync, _ => ArticlePageVM.HasNextPage);
			PrevPageCommand = new AsyncDelegateCommand(PrevPageAsync, _ => ArticlePageVM.HasPrevPage);

			ArticlePageVM.PropertyChanged += new PropertyChangedEventHandler((obj, target) => RaisePageChanged());
		}

		public void RaisePageChanged()
		{
			RaisePropertyChanged(nameof(CurrentPage));
			RaisePropertyChanged(nameof(TotalPages));
		}

		private async Task NextPageAsync(object parameter = null)
		{
			PagingParams = ArticlePageVM.NextPagingParams;

			await Task.Factory.StartNew(() => LoadCommand.TryExecute(parameter));
		}

		private async Task PrevPageAsync(object parameter = null)
		{
			PagingParams = ArticlePageVM.PrevPagingParams;
			await Task.Factory.StartNew(() => LoadCommand.TryExecute(parameter));
		}
	}
}