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
		public ArticleViewModel ArticleViewModel { get; }

		private readonly IMediator _mediator;

		private PaginatedList<GetArticleModel> _articlePage;
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
		private PagingParams _pagingParams;

		public ArticlesViewModel(GetArticleModel article, IMediator mediator)
		{
			ArticleViewModel = new ArticleViewModel();
			_mediator = mediator;
			_pagingParams = new PagingParams();
			ArticlePage = new PaginatedList<GetArticleModel>();

			Articles = new ObservableCollection<GetArticleModel>();

			LoadCommand = new AsyncDelegateCommand(LoadArticlesAsync);
			NextPageCommand = new AsyncDelegateCommand(NextPageAsync, CanLoadNextPage);
			PrevPageCommand = new AsyncDelegateCommand(PrevPageAsync, CanLoadPrevPage);
		}

		#region Public Properties
		public int CurrentPageNumber { get => ArticlePage.PageIndex; }
		public int TotalPages { get => ArticlePage.TotalPages; }
		public int NumberArticles { get => ArticlePage.TotalCount; }

		public ObservableCollection<GetArticleModel> Articles { get; private set; }

		


		#endregion

		#region Load Command
		public ICommand LoadCommand { get; }

		public async Task LoadArticlesAsync()
		{
			Articles.Clear();

			ArticlePage = await _mediator.Send(new GetArticlesWithPaginationQuery(_pagingParams));
			foreach (var article in ArticlePage.Items)
			{
				Articles.Add(article);
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
