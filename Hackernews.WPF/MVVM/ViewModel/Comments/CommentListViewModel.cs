using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.Model;
using Hackernews.WPF.MVVM.ViewModel.Comments;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Hackernews.WPF.ViewModels
{
	public class CommentListViewModel : BaseViewModel
	{
		public PagingParams PagingParams = new PagingParams();

		public CommentViewModel CommentViewModel { get; } = new CommentViewModel();

		public ObservableCollection<GetCommentModel> Comments { get; private set; } = new ObservableCollection<GetCommentModel>();

		public PaginatedListViewModel<GetCommentModel> CommentPageVM { get; set; }


		public LoadCommentsCommand LoadCommand { get; }
		public AsyncDelegateCommand NextPageCommand { get; }
		public AsyncDelegateCommand PrevPageCommand { get; }

		public CommentListViewModel(IApiClient apiClient)
		{
			CommentPageVM = new PaginatedListViewModel<GetCommentModel>();

			LoadCommand = new LoadCommentsCommand(this, apiClient);
			NextPageCommand = new AsyncDelegateCommand(NextPageAsync, () => CommentPageVM.HasNextPage);
			PrevPageCommand = new AsyncDelegateCommand(PrevPageAsync, () => CommentPageVM.HasPrevPage);
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
