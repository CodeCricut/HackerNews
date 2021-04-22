using Hackernews.WPF.ApiClients;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel.Boards
{
	public class LoadBoardsByIdsCommand : LoadEntityListByIdsCommand<BoardViewModel, GetBoardModel>
	{
		private readonly IApiClient _apiClient;
		private readonly PrivateUserViewModel _userVm;

		public LoadBoardsByIdsCommand(EntityListViewModel<BoardViewModel, GetBoardModel> listVM,
			IApiClient apiClient,
			PrivateUserViewModel userVm) : base(listVM)
		{
			_apiClient = apiClient;
			_userVm = userVm;
		}

		public override Task<PaginatedList<GetBoardModel>> LoadEntityModelsAsync(List<int> ids, PagingParams pagingParams)
		{
			return _apiClient.GetAsync<GetBoardModel>(ids, pagingParams, "boards");
		}

		public override BoardViewModel ConstructEntityViewModel(GetBoardModel getModel)
		{
			return new BoardViewModel(_apiClient, _userVm) { Board = getModel };
		}
	}

	public class LoadBoardsCommand : LoadEntityListCommand<BoardViewModel, GetBoardModel>
	{
		private readonly IApiClient _apiClient;
		private readonly PrivateUserViewModel _userVM;

		public LoadBoardsCommand(EntityListViewModel<BoardViewModel, GetBoardModel> listVM,
			IApiClient apiClient,
			PrivateUserViewModel userVM) : base(listVM)
		{
			_apiClient = apiClient;
			_userVM = userVM;
		}

		public override BoardViewModel ConstructEntityViewModel(GetBoardModel getModel)
		{
			return new BoardViewModel(_apiClient, _userVM) { Board = getModel };
		}

		public override Task<PaginatedList<GetBoardModel>> LoadEntityModelsAsync(PagingParams pagingParams)
		{
			return _apiClient.GetPageAsync<GetBoardModel>(pagingParams, "boards");
		}
	}
}
