using Hackernews.WPF.Factories.ViewModels;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.ApiConsumer.Core;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.WPF.MessageBus.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel.Boards
{
	public class LoadBoardsByIdsCommand : LoadEntityListByIdsCommand<BoardViewModel, GetBoardModel>
	{
		private readonly IBoardApiClient _boardApiClient;
		private readonly IBoardViewModelFactory _boardViewModelFactory;

		public LoadBoardsByIdsCommand(EntityListViewModel<BoardViewModel, GetBoardModel> listVM,
			IBoardApiClient boardApiClient,
			IBoardViewModelFactory boardViewModelFactory
			) : base(listVM)
		{
			_boardApiClient = boardApiClient;
			_boardViewModelFactory = boardViewModelFactory;
		}

		public override Task<PaginatedList<GetBoardModel>> LoadEntityModelsAsync(List<int> ids, PagingParams pagingParams)
		{
			return _boardApiClient.GetByIdsAsync(ids, pagingParams);
		}

		public override BoardViewModel ConstructEntityViewModel(GetBoardModel getModel)
		{
			var board = _boardViewModelFactory.Create();
			board.Board = getModel;
			return board;
		}
	}

	public class LoadBoardsCommand : LoadEntityListCommand<BoardViewModel, GetBoardModel>
	{
		private readonly IBoardApiClient _boardApiClient;
		private readonly IBoardViewModelFactory _boardViewModelFactory;

		public LoadBoardsCommand(EntityListViewModel<BoardViewModel, GetBoardModel> listVM,
			IBoardApiClient boardApiClient,
			IBoardViewModelFactory boardViewModelFactory
			) : base(listVM)
		{
			_boardApiClient = boardApiClient;
			_boardViewModelFactory = boardViewModelFactory;
		}

		public override BoardViewModel ConstructEntityViewModel(GetBoardModel getModel)
		{
			var board = _boardViewModelFactory.Create();
			board.Board = getModel;
			return board;
		}

		public override Task<PaginatedList<GetBoardModel>> LoadEntityModelsAsync(PagingParams pagingParams)
		{
			return _boardApiClient.GetPageAsync(pagingParams);
		}
	}
}
