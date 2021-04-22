using Hackernews.WPF.Core;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.Model;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel
{
	/// <summary>
	/// The VM for ArticleListControl, including pagination.
	/// </summary>
	public class ArticleListViewModel : BaseViewModel
	{
		/// <summary>
		/// The currently loaded <see cref="ArticleViewModel"/>s.
		/// </summary>
		public ObservableCollection<ArticleViewModel> Articles { get; private set; } = new ObservableCollection<ArticleViewModel>();

		public PaginatedListViewModel<GetArticleModel> ArticlePageVM { get; }

		public BaseCommand LoadCommand { get; set; }
		//public AsyncDelegateCommand PrevPageCommand { get; }
		//public AsyncDelegateCommand NextPageCommand { get; }

		//public int CurrentPage
		//{
		//	get => ArticlePageVM.CurrentPage;
		//}
		//public int TotalPages
		//{
		//	get => ArticlePageVM.TotalPages;
		//}

		public ArticleListViewModel(CreateBaseCommand<ArticleListViewModel> createLoadCommand)
		{
			LoadCommand = createLoadCommand(this);

			ArticlePageVM = new PaginatedListViewModel<GetArticleModel>(LoadCommand);

			//NextPageCommand = new AsyncDelegateCommand(NextPageAsync, _ => ArticlePageVM.HasNextPage);
			//PrevPageCommand = new AsyncDelegateCommand(PrevPageAsync, _ => ArticlePageVM.HasPrevPage);

			ArticlePageVM.PropertyChanged += new PropertyChangedEventHandler((obj, target) => RaisePageChanged());
		}

		public void RaisePageChanged()
		{
			ArticlePageVM.RaisePropertyChanged(nameof(ArticlePageVM.CurrentPage));
			ArticlePageVM.RaisePropertyChanged(nameof(ArticlePageVM.TotalPages));
		}

		//private async Task NextPageAsync(object parameter = null)
		//{
		//	await Task.Factory.StartNew(() =>
		//	{
		//		ArticlePageVM.TryLoadNextPage();
		//		LoadCommand.TryExecute(parameter);
		//	});
		//}

		//private async Task PrevPageAsync(object parameter = null)
		//{
		//	await Task.Factory.StartNew(() =>
		//	{
		//		ArticlePageVM.TryLoadPrevPage();
		//		LoadCommand.TryExecute(parameter);
		//	});
		//}
	}
}
