using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hackernews.WPF.ViewModels
{
	public class ArticleListViewModel : BaseViewModel
	{
		private readonly PrivateUserViewModel _userVM;

		public PageViewModel<GetArticleModel> ArticlePage { get; set; }
		public ObservableCollection<ArticleViewModel> ArticleViewModels { get; private set; } = new ObservableCollection<ArticleViewModel>();

		public AsyncDelegateCommand LoadCommand { get; }

		public ArticleListViewModel(IApiClient apiClient, PrivateUserViewModel userVM)
		{
			SelectedArticleViewModel = new ArticleViewModel(userVM);
			ArticlePage = new PageViewModel<GetArticleModel>("articles", userVM, apiClient);
			
			LoadCommand = new AsyncDelegateCommand(LoadArticlesAsync);
			_userVM = userVM;
		}

		private ArticleViewModel _articleViewModel;
		public ArticleViewModel SelectedArticleViewModel { 
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

		public async Task LoadArticlesAsync()
		{
			ArticleViewModels.Clear();

			await ArticlePage.LoadEntitysAsync();

			foreach (var article in ArticlePage.EntityPage.Items)
			{
				var articleVM = new ArticleViewModel(_userVM)
				{
					Article = article
				};
				ArticleViewModels.Add(articleVM);
			}
		}
	}
}
