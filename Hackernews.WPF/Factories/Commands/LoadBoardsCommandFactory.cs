using Hackernews.WPF.Core;
using Hackernews.WPF.Factories.ViewModels;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Boards;
using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.Domain.Common.Models.Boards;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.Factories.Commands
{
	public interface ILoadBoardsCommandFactory
	{
		BaseCommand Create(EntityListViewModel<BoardViewModel, GetBoardModel> boardListVm, LoadEntityListEnum loadEntityType);
	}

	public class LoadBoardsCommandFactory : ILoadBoardsCommandFactory
	{
		private readonly IBoardApiClient _boardApiClient;
		private readonly IBoardViewModelFactory _boardViewModelFactory;

		public LoadBoardsCommandFactory(IBoardApiClient boardApiClient,
			IBoardViewModelFactory boardViweModelFactory)
		{
			_boardApiClient = boardApiClient;
			_boardViewModelFactory = boardViweModelFactory;
		}

		public BaseCommand Create(EntityListViewModel<BoardViewModel, GetBoardModel> boardListVm, LoadEntityListEnum loadEntityType)
		{
			return loadEntityType switch
			{
				LoadEntityListEnum.LoadAll => new LoadBoardsCommand(boardListVm, _boardApiClient, _boardViewModelFactory),
				_ => new LoadBoardsByIdsCommand(boardListVm, _boardApiClient, _boardViewModelFactory),
			};
		}
	}
}
