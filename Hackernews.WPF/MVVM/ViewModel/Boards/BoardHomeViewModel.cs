using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core;
using Hackernews.WPF.Core.Commands;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
				BoardViewModel.LoadBoardCommand.Execute(_);
			});
		}

		private void LoadBoardArticles() => BoardArticleListVM.LoadCommand.Execute(BoardViewModel.Board.ArticleIds);

		public BoardHomeViewModel(BoardViewModel boardVM, IApiClient apiClient, PrivateUserViewModel privateUserVM)
		{
			BoardViewModel = boardVM;

			CreateBaseCommand<ArticleListViewModel> createLoadArticlesCommand = articleListVm => new LoadArticlesByIdsCommand(articleListVm, apiClient, privateUserVM);
			BoardArticleListVM = new ArticleListViewModel(createLoadArticlesCommand);

			LoadBoardCommand = new AsyncDelegateCommand(LoadBoard);
		}
	}
}
