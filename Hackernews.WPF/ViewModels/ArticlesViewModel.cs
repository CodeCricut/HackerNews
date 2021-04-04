using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using HackerNews.Application.Articles.Queries.GetArticle;
using HackerNews.Application.Articles.Queries.GetArticlesWithPagination;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hackernews.WPF.ViewModels
{
	public class ArticlesViewModel : BaseViewModel
	{
		public IApiClient _apiClient { get; }
		public ArticleViewModel ArticleViewModel { get; }


		private PaginatedList<GetArticleModel> _articlePage = new PaginatedList<GetArticleModel>();
		private PaginatedList<GetArticleModel> ArticlePage
		{
			get => _articlePage;
			set
			{
				if (_articlePage != value)
				{
					_articlePage = value;
					RaisePropertyChanged();
					RaisePropertyChanged(nameof(CurrentPageNumber));
					RaisePropertyChanged(nameof(TotalPages));
					RaisePropertyChanged(nameof(NumberArticles));
				}
			}
		}
		private PagingParams _pagingParams = new PagingParams();

		public ArticlesViewModel(IApiClient apiClient)
		{
			_apiClient = apiClient;
			ArticleViewModel = new ArticleViewModel();
			
			LoadCommand = new AsyncDelegateCommand(LoadArticlesAsync);
			NextPageCommand = new AsyncDelegateCommand(NextPageAsync, CanLoadNextPage);
			PrevPageCommand = new AsyncDelegateCommand(PrevPageAsync, CanLoadPrevPage);
		}

		#region Public Properties
		public int CurrentPageNumber { get => ArticlePage.PageIndex; }
		public int TotalPages { get => ArticlePage.TotalPages; }
		public int NumberArticles { get => ArticlePage.TotalCount; }

		public ObservableCollection<GetArticleModel> Articles { get; private set; } = new ObservableCollection<GetArticleModel>();
		#endregion

		#region Load Command
		public ICommand LoadCommand { get; }

		public async Task LoadArticlesAsync()
		{
			Articles.Clear();

			ArticlePage = await _apiClient.GetPageAsync<GetArticleModel>(_pagingParams, "articles");

			foreach (var board in ArticlePage.Items)
			{
				Articles.Add(board);
			}

			NextPageCommand.RaiseCanExecuteChanged();
			PrevPageCommand.RaiseCanExecuteChanged();
		}
		#endregion

		#region NextPage Command
		public AsyncDelegateCommand NextPageCommand { get; }

		private async Task NextPageAsync()
		{
			_pagingParams = ArticlePage.NextPagingParams;
			await LoadArticlesAsync();

		}

		private bool CanLoadNextPage()
		{
			return ArticlePage.HasNextPage;
		}
		#endregion

		#region PrevPage Command
		public AsyncDelegateCommand PrevPageCommand { get; }

		private async Task PrevPageAsync()
		{
			_pagingParams = ArticlePage.PreviousPagingParams;
			await LoadArticlesAsync();

		}

		private bool CanLoadPrevPage()
		{
			return ArticlePage.HasPreviousPage;
		}
		#endregion
	}
}
