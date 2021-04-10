using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using HackerNews.Application.Boards.Queries.GetBoardsWithPagination;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
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
	public class BoardsListViewModel : BaseViewModel
	{
		public IApiClient _apiClient { get; }
		public BoardViewModel BoardViewModel { get; set; }


		private PaginatedList<GetBoardModel> _boardPage = new PaginatedList<GetBoardModel>();
		private PaginatedList<GetBoardModel> BoardPage
		{
			get => _boardPage;
			set
			{
				if (_boardPage != value)
				{
					_boardPage = value;
					RaisePropertyChanged();
					RaisePropertyChanged(nameof(CurrentPageNumber));
					RaisePropertyChanged(nameof(TotalPages));
					RaisePropertyChanged(nameof(NumberArticles));
				}
			}
		}
		private PagingParams _pagingParams = new PagingParams();

		public BoardsListViewModel(IApiClient apiClient)
		{
			_apiClient = apiClient;

			BoardViewModel = new BoardViewModel();

			LoadCommand = new AsyncDelegateCommand(LoadBoardsAsync);
			NextPageCommand = new AsyncDelegateCommand(NextPageAsync, CanLoadNextPage);
			PrevPageCommand = new AsyncDelegateCommand(PrevPageAsync, CanLoadPrevPage);
		}

		#region Public Properties
		public int CurrentPageNumber { get => BoardPage.PageIndex; }
		public int TotalPages { get => BoardPage.TotalPages; }
		public int NumberArticles { get => BoardPage.TotalCount; }

		public ObservableCollection<GetBoardModel> Boards { get; private set; } = new ObservableCollection<GetBoardModel>();
		#endregion

		#region Load Command
		public ICommand LoadCommand { get; }

		public async Task LoadBoardsAsync()
		{
			Boards.Clear();

			BoardPage = await _apiClient.GetPageAsync<GetBoardModel>(_pagingParams, "boards");
				
			foreach (var board in BoardPage.Items)
			{
				Boards.Add(board);
			}

			NextPageCommand.RaiseCanExecuteChanged();
			PrevPageCommand.RaiseCanExecuteChanged();
		}
		#endregion

		#region NextPage Command
		public AsyncDelegateCommand NextPageCommand { get; }

		private async Task NextPageAsync()
		{
			_pagingParams = BoardPage.NextPagingParams;
			await LoadBoardsAsync();

		}

		private bool CanLoadNextPage()
		{
			return BoardPage.HasNextPage;
		}
		#endregion

		#region PrevPage Command
		public AsyncDelegateCommand PrevPageCommand { get; }

		private async Task PrevPageAsync()
		{
			_pagingParams = BoardPage.PreviousPagingParams;
			await LoadBoardsAsync();

		}

		private bool CanLoadPrevPage()
		{
			return BoardPage.HasPreviousPage;
		}
		#endregion
	}
}
