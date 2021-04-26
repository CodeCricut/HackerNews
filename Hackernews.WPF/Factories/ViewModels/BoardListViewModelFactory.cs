using Hackernews.WPF.Factories.Commands;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Common;

namespace Hackernews.WPF.Factories.ViewModels
{
	public interface IBoardListViewModelFactory
	{
		BoardListViewModel Create(LoadEntityListEnum loadEntityType);
	}

	public class BoardListViewModelFactory : IBoardListViewModelFactory
	{
		private readonly ILoadBoardsCommandFactory _loadBoardsCommandFactory;

		public BoardListViewModelFactory(ILoadBoardsCommandFactory loadBoardsCommandFactory)
		{
			_loadBoardsCommandFactory = loadBoardsCommandFactory;
		}

		public BoardListViewModel Create(LoadEntityListEnum loadEntityType)
			=> new BoardListViewModel(createLoadCommand: entityVm => _loadBoardsCommandFactory.Create(entityVm, loadEntityType));
	}
}
