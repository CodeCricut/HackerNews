using Hackernews.WPF.ApiClients;
using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel.Comments
{
	public class LoadCommentsByIdsCommand : LoadEntityListByIdsCommand<CommentViewModel, GetCommentModel>
	{
		private readonly IApiClient _apiClient;

		public LoadCommentsByIdsCommand(EntityListViewModel<CommentViewModel, GetCommentModel> listVM,
			IApiClient apiClient) : base(listVM)
		{
			_apiClient = apiClient;
		}

		public override Task<PaginatedList<GetCommentModel>> LoadEntityModelsAsync(List<int> ids, PagingParams pagingParams)
		{
			return _apiClient.GetAsync<GetCommentModel>(ids, pagingParams, "comments");
		}

		public override CommentViewModel ConstructEntityViewModel(GetCommentModel getModel)
		{
			return new CommentViewModel() { Comment = getModel };
		}
	}

	public class LoadCommentsCommand : LoadEntityListCommand<CommentViewModel, GetCommentModel>
	{
		private readonly IApiClient _apiClient;

		public LoadCommentsCommand(EntityListViewModel<CommentViewModel, GetCommentModel> listVM,
			IApiClient apiClient) : base(listVM)
		{
			_apiClient = apiClient;
		}

		public override CommentViewModel ConstructEntityViewModel(GetCommentModel getModel)
		{
			return new CommentViewModel() { Comment = getModel };
		}

		public override Task<PaginatedList<GetCommentModel>> LoadEntityModelsAsync(PagingParams pagingParams)
		{
			return _apiClient.GetPageAsync<GetCommentModel>(pagingParams, "comments");
		}
	}
}
