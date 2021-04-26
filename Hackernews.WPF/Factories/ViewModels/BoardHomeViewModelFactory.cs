using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Boards;

namespace Hackernews.WPF.Factories.ViewModels
{
	public interface IBoardHomeViewModelFactory
	{
		//BoardHomeViewModel Create();
		BoardHomeViewModel Create(BoardViewModel boardVm);
	}

	public class BoardHomeViewModelFactory : IBoardHomeViewModelFactory
	{
		private readonly IArticleListViewModelFactory _articleListViewModelFactory;

		public BoardHomeViewModelFactory(
			//IBoardViewModelFactory boardVmFactory,
			IArticleListViewModelFactory articleListViewModelFactory)
		{
			_articleListViewModelFactory = articleListViewModelFactory;
		}

		//public BoardHomeViewModel Create()
		//	=> new BoardHomeViewModel(_boardVmFactory.Create(), _articleListViewModelFactory);

		public BoardHomeViewModel Create(BoardViewModel boardVm)
			=> new BoardHomeViewModel(boardVm, _articleListViewModelFactory);
	}
}
