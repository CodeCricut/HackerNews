using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class PageViewModel<TEntity> : BaseViewModel where TEntity : class
	{
		//public AsyncDelegateCommand LoadCommand { get; }
		public DelegateCommand PrevPageCommand { get; }
		public DelegateCommand NextPageCommand { get; }
		
		public PageViewModel(string apiPageEndpoint, PrivateUserViewModel userVM, IApiClient apiClient)
		{
			_apiPageEndpoint = apiPageEndpoint;
			_userVM = userVM;
			_apiClient = apiClient;

			//LoadCommand = new AsyncDelegateCommand(LoadEntitysAsync);
			NextPageCommand = new DelegateCommand(NextPage, () => _entityPage.HasNextPage);
			PrevPageCommand = new DelegateCommand(PrevPage, () => _entityPage.HasPreviousPage);
		}

		private PaginatedList<TEntity> _entityPage = new PaginatedList<TEntity>();
		public PaginatedList<TEntity> EntityPage
		{
			get => _entityPage;
			set
			{
				if (_entityPage != value)
				{
					_entityPage = value;
					RaisePropertyChanged();
					//RaisePropertyChanged(nameof(CurrentPageNumber));
					//RaisePropertyChanged(nameof(TotalPages));
					//RaisePropertyChanged(nameof(NumberEntitys));
				}
			}
		}
		private PagingParams _pagingParams = new PagingParams();
		private readonly string _apiPageEndpoint;
		private readonly PrivateUserViewModel _userVM;
		private readonly IApiClient _apiClient;

		//public int CurrentPageNumber { get => EntityPage.PageIndex; }
		//public int TotalPages { get => EntityPage.TotalPages; }
		//public int NumberEntitys { get => EntityPage.TotalCount; }

		public async Task LoadEntitysAsync()
		{
			EntityPage = await _apiClient.GetPageAsync<TEntity>(_pagingParams, _apiPageEndpoint);

			await Task.Factory.StartNew(() => _userVM.TryLoadUserCommand.Execute(null));

			NextPageCommand.RaiseCanExecuteChanged();
			PrevPageCommand.RaiseCanExecuteChanged();
		}

		private void NextPage()
		{
			_pagingParams = EntityPage.NextPagingParams;
			//await LoadEntitysAsync();
		}

		private void PrevPage()
		{
			_pagingParams = EntityPage.PreviousPagingParams;
			//await LoadEntitysAsync();
		}

	}
}
