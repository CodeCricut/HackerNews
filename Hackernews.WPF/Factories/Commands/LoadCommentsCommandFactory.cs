using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Comments;
using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.WPF.Core.Commands;

namespace Hackernews.WPF.Factories.Commands
{
	public interface ILoadCommentsCommandFactory
	{
		BaseCommand Create(EntityListViewModel<CommentViewModel, GetCommentModel> userListVm, LoadEntityListEnum loadEntityType);
	}

	public class LoadCommentsCommandFactory : ILoadCommentsCommandFactory
	{
		private readonly ICommentApiClient _commentApiClient;

		public LoadCommentsCommandFactory(ICommentApiClient commentApiClient)
		{
			_commentApiClient = commentApiClient;
		}

		public BaseCommand Create(EntityListViewModel<CommentViewModel, GetCommentModel> commentListVm, LoadEntityListEnum loadEntityType)
			=> loadEntityType switch
			{
				LoadEntityListEnum.LoadAll => new LoadCommentsCommand(commentListVm, _commentApiClient),
				_ => new LoadCommentsByIdsCommand(commentListVm, _commentApiClient),
			};
	}
}
