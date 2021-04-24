using Hackernews.WPF.ApiClients;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.WPF.MessageBus.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel.Boards
{
	public class LoadBoardsByIdsCommand : LoadEntityListByIdsCommand<BoardViewModel, GetBoardModel>
	{
		private readonly IViewManager _viewManager;
		private readonly IApiClient _apiClient;
		private readonly IEventAggregator _ea;
		private readonly PrivateUserViewModel _userVm;

		public LoadBoardsByIdsCommand(EntityListViewModel<BoardViewModel, GetBoardModel> listVM,
			IViewManager viewManager,
			IApiClient apiClient,
			IEventAggregator ea,
			PrivateUserViewModel userVm) : base(listVM)
		{
			_viewManager = viewManager;
			_apiClient = apiClient;
			_ea = ea;
			_userVm = userVm;
		}

		public override Task<PaginatedList<GetBoardModel>> LoadEntityModelsAsync(List<int> ids, PagingParams pagingParams)
		{
			return _apiClient.GetAsync<GetBoardModel>(ids, pagingParams, "boards");
		}

		public override BoardViewModel ConstructEntityViewModel(GetBoardModel getModel)
		{
			return new BoardViewModel(_ea, _viewManager, _apiClient, _userVm) { Board = getModel };
		}
	}

	public class LoadBoardsCommand : LoadEntityListCommand<BoardViewModel, GetBoardModel>
	{
		private readonly IViewManager _viewManager;
		private readonly IApiClient _apiClient;
		private readonly IEventAggregator _ea;
		private readonly PrivateUserViewModel _userVM;

		public LoadBoardsCommand(EntityListViewModel<BoardViewModel, GetBoardModel> listVM,
			IViewManager viewManager,
			IApiClient apiClient,
			IEventAggregator ea,
			PrivateUserViewModel userVM) : base(listVM)
		{
			_viewManager = viewManager;
			_apiClient = apiClient;
			_ea = ea;
			_userVM = userVM;
		}

		public override BoardViewModel ConstructEntityViewModel(GetBoardModel getModel)
		{
			return new BoardViewModel(_ea, _viewManager, _apiClient, _userVM) { Board = getModel };
		}

		public override Task<PaginatedList<GetBoardModel>> LoadEntityModelsAsync(PagingParams pagingParams)
		{
			return _apiClient.GetPageAsync<GetBoardModel>(pagingParams, "boards");
		}
	}
}
