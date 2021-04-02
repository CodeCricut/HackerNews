using Hackernews.WPF.Helpers;
using HackerNews.Application.Comments.Queries.GetCommentsWithPagination;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hackernews.WPF.ViewModels
{
	public class CommentListViewModel : BaseViewModel
	{
		public CommentViewModel CommentViewModel { get; }

		private readonly IMediator _mediator;

		private PaginatedList<GetCommentModel> _commentPage = new PaginatedList<GetCommentModel>();
		private PaginatedList<GetCommentModel> CommentPage
		{
			get => _commentPage;
			set
			{
				if (_commentPage != value)
				{
					_commentPage = value;
					RaisePropertyChanged();
					RaisePropertyChanged(nameof(CurrentPageNumber));
					RaisePropertyChanged(nameof(TotalPages));
					RaisePropertyChanged(nameof(NumberComments));
				}
			}
		}
		private PagingParams _pagingParams = new PagingParams();

		public CommentListViewModel(IMediator mediator)
		{
			CommentViewModel = new CommentViewModel();
			_mediator = mediator;

			LoadCommand = new AsyncDelegateCommand(LoadCommentsAsync);
			NextPageCommand = new AsyncDelegateCommand(NextPageAsync, CanLoadNextPage);
			PrevPageCommand = new AsyncDelegateCommand(PrevPageAsync, CanLoadPrevPage);
		}

		#region Public Properties
		public int CurrentPageNumber { get => CommentPage.PageIndex; }
		public int TotalPages { get => CommentPage.TotalPages; }
		public int NumberComments { get => CommentPage.TotalCount; }

		public ObservableCollection<GetCommentModel> Comments { get; private set; } = new ObservableCollection<GetCommentModel>();
		#endregion

		#region Load Command
		public ICommand LoadCommand { get; }

		public async Task LoadCommentsAsync()
		{
			Comments.Clear();

			CommentPage = await _mediator.Send(new GetCommentsWithPaginationQuery(_pagingParams));
			foreach (var comment in CommentPage.Items)
			{
				Comments.Add(comment);
			}

			NextPageCommand.RaiseCanExecuteChanged();
			PrevPageCommand.RaiseCanExecuteChanged();
		}
		#endregion

		#region NextPage Command
		public AsyncDelegateCommand NextPageCommand { get; }

		private async Task NextPageAsync()
		{
			_pagingParams = CommentPage.NextPagingParams;
			await LoadCommentsAsync();

		}

		private bool CanLoadNextPage()
		{
			return CommentPage.HasNextPage;
		}
		#endregion

		#region PrevPage Command
		public AsyncDelegateCommand PrevPageCommand { get; }

		private async Task PrevPageAsync()
		{
			_pagingParams = CommentPage.PreviousPagingParams;
			await LoadCommentsAsync();

		}

		private bool CanLoadPrevPage()
		{
			return CommentPage.HasPreviousPage;
		}
		#endregion
	}
}
