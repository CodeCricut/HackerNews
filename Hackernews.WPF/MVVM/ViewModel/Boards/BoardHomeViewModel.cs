using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core;
using Hackernews.WPF.Core.Commands;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.ViewModels;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel.Boards
{
	public class BoardHomeViewModel : BaseViewModel
	{
		// article list vm
		public ArticleListViewModel BoardArticleListVM { get; }

		// Board vm
		public BoardViewModel BoardViewModel { get;  }

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

		public BoardHomeViewModel(BoardViewModel boardVM, IApiClient apiClient, PrivateUserViewModel privateUserVM)
		{
			BoardViewModel = boardVM;

			CreateBaseCommand<ArticleListViewModel> createLoadArticlesCommand = articleListVm => new LoadArticlesByIdsCommand(articleListVm, apiClient, privateUserVM);

			//CreateBaseCommand<PaginatedListViewModel<GetArticleModel>> createLoadArticlePageCommand = vm => new LoadArticlePageCommand(vm, apiClient);
			BoardArticleListVM = new ArticleListViewModel(createLoadArticlesCommand);

			LoadBoardCommand = new AsyncDelegateCommand(LoadBoard);
		}
	}
}
