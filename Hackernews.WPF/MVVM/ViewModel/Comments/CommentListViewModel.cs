using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.WPF.Core.Commands;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class CommentListViewModel : EntityListViewModel<CommentViewModel, GetCommentModel>
	{
		public CommentListViewModel(CreateBaseCommand<EntityListViewModel<CommentViewModel, GetCommentModel>> createLoadCommand) : base(createLoadCommand)
		{
		}
	}
}
