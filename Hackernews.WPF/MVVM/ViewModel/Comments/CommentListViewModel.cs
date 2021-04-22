using Hackernews.WPF.Core;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.Model;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class CommentListViewModel : BaseViewModel
	{
		//public PagingParams PagingParams = new PagingParams();

		public CommentViewModel CommentViewModel { get; } = new CommentViewModel();

		public ObservableCollection<CommentViewModel> Comments { get; private set; } = new ObservableCollection<CommentViewModel>();

		public PaginatedListViewModel<GetCommentModel> CommentPageVM { get; set; }


		public BaseCommand LoadCommand { get; set; }
		//public AsyncDelegateCommand NextPageCommand { get; }
		//public AsyncDelegateCommand PrevPageCommand { get; }

		//public int CurrentPage
		//{
		//	get => CommentPageVM.CurrentPage;
		//}
		//public int TotalPages
		//{
		//	get => CommentPageVM.TotalPages;
		//}

		public CommentListViewModel(CreateBaseCommand<CommentListViewModel> createLoadCommand)
		{
			LoadCommand = createLoadCommand(this);

			CommentPageVM = new PaginatedListViewModel<GetCommentModel>(LoadCommand);

			//NextPageCommand = new AsyncDelegateCommand(NextPageAsync, _ => CommentPageVM.HasNextPage);
			//PrevPageCommand = new AsyncDelegateCommand(PrevPageAsync, _ => CommentPageVM.HasPrevPage);

			CommentPageVM.PropertyChanged += new PropertyChangedEventHandler((obj, target) => RaisePageChanged());
		}

		public void RaisePageChanged()
		{
			CommentPageVM.RaisePropertyChanged(nameof(CommentPageVM.CurrentPage));
			CommentPageVM.RaisePropertyChanged(nameof(CommentPageVM.TotalPages));
		}

		//private async Task NextPageAsync(object parameter = null)
		//{
		//	PagingParams = CommentPageVM.NextPagingParams;
		//	await Task.Factory.StartNew(() => LoadCommand.TryExecute(parameter));
		//}

		//private async Task PrevPageAsync(object parameter = null)
		//{
		//	PagingParams = CommentPageVM.PrevPagingParams;
		//	await Task.Factory.StartNew(() => LoadCommand.TryExecute(parameter));
		//}
	}
}
