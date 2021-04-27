using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Boards;

namespace Hackernews.WPF.Factories.ViewModels
{
	public interface IBoardHomeViewModelFactory
	{
		BoardHomeViewModel Create(BoardViewModel boardVm);
	}

	public class BoardHomeViewModelFactory : IBoardHomeViewModelFactory
	{
		private readonly IArticleListViewModelFactory _articleListViewModelFactory;
		private readonly IArticleCreationViewModelFactory _articleCreationViewModelFactory;

		public BoardHomeViewModelFactory(
			IArticleListViewModelFactory articleListViewModelFactory,
			IArticleCreationViewModelFactory articleCreationViewModelFactory)
		{
			_articleListViewModelFactory = articleListViewModelFactory;
			_articleCreationViewModelFactory = articleCreationViewModelFactory;
		}

		public BoardHomeViewModel Create(BoardViewModel boardVm)
			=> new BoardHomeViewModel(boardVm, _articleListViewModelFactory, _articleCreationViewModelFactory);
	}
}
