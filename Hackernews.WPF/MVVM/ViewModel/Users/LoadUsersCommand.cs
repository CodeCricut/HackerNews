using Hackernews.WPF.ApiClients;
using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class LoadUsersCommand : LoadEntityListCommand<PublicUserViewModel, GetPublicUserModel>
	{
		private readonly IApiClient _apiClient;

		public LoadUsersCommand(EntityListViewModel<PublicUserViewModel, GetPublicUserModel> listVM,
			IApiClient apiClient) : base(listVM)
		{
			_apiClient = apiClient;
		}

		public override PublicUserViewModel ConstructEntityViewModel(GetPublicUserModel getModel)
		{
			return new PublicUserViewModel(_apiClient) { User = getModel };
		}

		public override Task<PaginatedList<GetPublicUserModel>> LoadEntityModelsAsync(PagingParams pagingParams)
		{
			return _apiClient.GetPageAsync<GetPublicUserModel>(pagingParams, "users");
		}
	}
}
