using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core.Commands;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.WPF.MessageBus.Core;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel.Boards
{
	public class BoardHomeViewModel : BaseViewModel
	{
		// article list vm
		public EntityListViewModel<ArticleViewModel, GetArticleModel> BoardArticleListVM { get; }

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

		public BoardHomeViewModel(IEventAggregator ea, BoardViewModel boardVM, IApiClient apiClient, PrivateUserViewModel privateUserVM)
		{
			BoardViewModel = boardVM;

			BoardArticleListVM = new EntityListViewModel<ArticleViewModel, GetArticleModel>(createLoadCommand: entityListVm => new LoadArticlesByIdsCommand(entityListVm, apiClient, ea, privateUserVM));

			LoadBoardCommand = new AsyncDelegateCommand(LoadBoard);
		}
	}
}
