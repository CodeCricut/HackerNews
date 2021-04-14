using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Core;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.Model;
using Hackernews.WPF.MVVM.ViewModel.Comments;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class CommentListViewModel : BaseViewModel, IPageNavigatorViewModel
	{
		public PagingParams PagingParams = new PagingParams();

		public CommentViewModel CommentViewModel { get; } = new CommentViewModel();

		public ObservableCollection<GetCommentModel> Comments { get; private set; } = new ObservableCollection<GetCommentModel>();

		public PaginatedListViewModel<GetCommentModel> CommentPageVM { get; set; }


		public BaseCommand LoadCommand { get; set; }
		public AsyncDelegateCommand NextPageCommand { get; }
		public AsyncDelegateCommand PrevPageCommand { get; }

		public int CurrentPage
		{
			get => CommentPageVM.CurrentPageNumber;
		}
		public int TotalPages
		{
			get => CommentPageVM.TotalPages;
		}

		public CommentListViewModel(CreateBaseCommand<CommentListViewModel> createLoadCommand)
		{
			CommentPageVM = new PaginatedListViewModel<GetCommentModel>();

			LoadCommand = createLoadCommand(this);
			NextPageCommand = new AsyncDelegateCommand(NextPageAsync, () => CommentPageVM.HasNextPage);
			PrevPageCommand = new AsyncDelegateCommand(PrevPageAsync, () => CommentPageVM.HasPrevPage);

			CommentPageVM.PropertyChanged += new PropertyChangedEventHandler((obj, target) => RaisePageChanged());
		}

		public void RaisePageChanged()
		{
			RaisePropertyChanged(nameof(CurrentPage));
			RaisePropertyChanged(nameof(TotalPages));
		}

		private async Task NextPageAsync()
		{
			PagingParams = CommentPageVM.NextPagingParams;
			await Task.Factory.StartNew(() => LoadCommand.TryExecute());
		}

		private async Task PrevPageAsync()
		{
			PagingParams = CommentPageVM.PrevPagingParams;
			await Task.Factory.StartNew(() => LoadCommand.TryExecute());
		}
	}
}
