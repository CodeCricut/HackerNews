using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel.Comments
{
	public class LoadCommentsByIdsCommand : LoadEntityListByIdsCommand<CommentViewModel, GetCommentModel>
	{
		private readonly ICommentApiClient _commentApiClient;

		public LoadCommentsByIdsCommand(EntityListViewModel<CommentViewModel, GetCommentModel> listVM,
			ICommentApiClient commentApiClient) : base(listVM)
		{
			_commentApiClient = commentApiClient;
		}

		public override Task<PaginatedList<GetCommentModel>> LoadEntityModelsAsync(List<int> ids, PagingParams pagingParams)
		{
			return _commentApiClient.GetByIdsAsync(ids, pagingParams);
		}

		public override CommentViewModel ConstructEntityViewModel(GetCommentModel getModel)
		{
			return new CommentViewModel() { Comment = getModel };
		}
	}

	public class LoadCommentsCommand : LoadEntityListCommand<CommentViewModel, GetCommentModel>
	{
		private readonly ICommentApiClient _commentApiClient;

		public LoadCommentsCommand(EntityListViewModel<CommentViewModel, GetCommentModel> listVM,
			ICommentApiClient commentApiClient) : base(listVM)
		{
			_commentApiClient = commentApiClient;
		}

		public override CommentViewModel ConstructEntityViewModel(GetCommentModel getModel)
		{
			return new CommentViewModel() { Comment = getModel };
		}

		public override Task<PaginatedList<GetCommentModel>> LoadEntityModelsAsync(PagingParams pagingParams)
		{
			return _commentApiClient.GetPageAsync(pagingParams);
		}
	}
}
