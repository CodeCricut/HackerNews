using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core;
using Hackernews.WPF.Core.Commands;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.Model;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Hackernews.WPF.ViewModels
{
	public class ArticleListViewModel : BaseViewModel
	{
		public PagingParams PagingParams = new PagingParams();

		private ArticleViewModel _articleViewModel;
		public ArticleViewModel ArticleViewModel
		{
			get => _articleViewModel;
			set
			{
				if (_articleViewModel != value)
				{
					_articleViewModel = value;
					RaisePropertyChanged("");
				}
			}
		}

		public bool IsArticleSelected { get => ArticleViewModel != null; }

		public ObservableCollection<ArticleViewModel> Articles { get; private set; } = new ObservableCollection<ArticleViewModel>();

		public PaginatedListViewModel<GetArticleModel> ArticlePageVM { get; }

		public BaseCommand LoadCommand { get; }
		public AsyncDelegateCommand PrevPageCommand { get; }
		public AsyncDelegateCommand NextPageCommand { get; }

		public ArticleListViewModel(IApiClient apiClient, PrivateUserViewModel userVM)
		{
			ArticlePageVM = new PaginatedListViewModel<GetArticleModel>();

			LoadCommand = new LoadArticlesCommand(this, apiClient, userVM);
			NextPageCommand = new AsyncDelegateCommand(NextPageAsync, () => ArticlePageVM.HasNextPage);
			PrevPageCommand = new AsyncDelegateCommand(PrevPageAsync, () => ArticlePageVM.HasPrevPage);
		}

		private async Task NextPageAsync()
		{
			PagingParams = ArticlePageVM.NextPagingParams;

			await Task.Factory.StartNew(() => LoadCommand.TryExecute());
		}

		private async Task PrevPageAsync()
		{
			PagingParams = ArticlePageVM.PrevPagingParams;
			await Task.Factory.StartNew(() => LoadCommand.TryExecute());
		}
	}
}
