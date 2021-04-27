using Hackernews.WPF.Factories.ViewModels;
using Hackernews.WPF.MVVM.ViewModel.Articles;
using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.WPF.Core.Commands;
using HackerNews.WPF.Core.ViewModel;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel.Boards
{
	public class BoardHomeViewModel : BaseViewModel
	{

		// article list vm
		public EntityListViewModel<ArticleViewModel, GetArticleModel> BoardArticleListVM { get; }
		public ArticleCreationViewModel ArticleCreationVm { get; }

		// Board vm
		public BoardViewModel BoardViewModel { get; }

		// Load board/articles
		public AsyncDelegateCommand LoadBoardCommand { get; }

		private Task LoadBoard(object _ = null)
		{
			return Task.Factory.StartNew(() =>
			{
				LoadBoardArticles();
				BoardViewModel.LoadEntityCommand.Execute(_);
			});
		}

		private void LoadBoardArticles() => BoardArticleListVM.LoadCommand.Execute(BoardViewModel.Board.ArticleIds);

		public BoardHomeViewModel(
			BoardViewModel boardVM,
			IArticleListViewModelFactory articleListViewModelFactory,
			IArticleCreationViewModelFactory articleCreationViewModelFactory
			)
		{
			BoardViewModel = boardVM;
			BoardArticleListVM = articleListViewModelFactory.Create(LoadEntityListEnum.LoadByIds);
			ArticleCreationVm = articleCreationViewModelFactory.Create(boardVM.Board);

			LoadBoardCommand = new AsyncDelegateCommand(LoadBoard);
		}
	}
}
