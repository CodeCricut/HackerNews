using Hackernews.WPF.Factories.ViewModels;
using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public interface ILoadUserCommand { }
	public class LoadUsersByIdsCommand : LoadEntityListByIdsCommand<PublicUserViewModel, GetPublicUserModel>, ILoadUserCommand
	{
		private readonly IUserApiClient _userApiClient;
		private readonly IPublicUserViewModelFactory _publicUserViewModelFactory;

		public LoadUsersByIdsCommand(EntityListViewModel<PublicUserViewModel, GetPublicUserModel> listVM,
			IUserApiClient userApiClient,
			IPublicUserViewModelFactory publicUserViewModelFactory
			) : base(listVM)
		{
			_userApiClient = userApiClient;
			_publicUserViewModelFactory = publicUserViewModelFactory;
		}

		public override Task<PaginatedList<GetPublicUserModel>> LoadEntityModelsAsync(List<int> ids, PagingParams pagingParams)
			=> _userApiClient.GetByIdsAsync(ids, pagingParams);

		public override PublicUserViewModel ConstructEntityViewModel(GetPublicUserModel getModel)
		{
			var pubUserVm = _publicUserViewModelFactory.Create();
			pubUserVm.User = getModel;
			return pubUserVm;
		}
	}

	public class LoadUsersCommand : LoadEntityListCommand<PublicUserViewModel, GetPublicUserModel>, ILoadUserCommand
	{
		private readonly IUserApiClient _userApiClient;
		private readonly IPublicUserViewModelFactory _publicUserViewModelFactory;

		public LoadUsersCommand(EntityListViewModel<PublicUserViewModel, GetPublicUserModel> listVM,
			IUserApiClient userApiClient,
			IPublicUserViewModelFactory publicUserViewModelFactory
			) : base(listVM)
		{
			_userApiClient = userApiClient;
			_publicUserViewModelFactory = publicUserViewModelFactory;
		}

		public override Task<PaginatedList<GetPublicUserModel>> LoadEntityModelsAsync(PagingParams pagingParams)
			=> _userApiClient.GetPageAsync(pagingParams);

		public override PublicUserViewModel ConstructEntityViewModel(GetPublicUserModel getModel)
		{
			var pubUserVm = _publicUserViewModelFactory.Create();
			pubUserVm.User = getModel;
			return pubUserVm;
		}
	}
}
