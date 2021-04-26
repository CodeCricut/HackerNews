using Hackernews.WPF.Core;
using Hackernews.WPF.Factories.ViewModels;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.Factories.Commands
{
	public interface ILoadUsersCommandFactory
	{
		BaseCommand Create(EntityListViewModel<PublicUserViewModel, GetPublicUserModel> userListVm, LoadEntityListEnum loadEntityType);
	}

	public class LoadUsersCommandFactory : ILoadUsersCommandFactory
	{
		private readonly IUserApiClient _userApiClient;
		private readonly IPublicUserViewModelFactory _publicUserViewModelFactory;

		public LoadUsersCommandFactory(IUserApiClient userApiClient, IPublicUserViewModelFactory publicUserViewModelFactory)
		{
			_userApiClient = userApiClient;
			_publicUserViewModelFactory = publicUserViewModelFactory;
		}

		public BaseCommand Create(EntityListViewModel<PublicUserViewModel, GetPublicUserModel> userListVm, LoadEntityListEnum loadEntityType)
		{
			return loadEntityType switch
			{
				LoadEntityListEnum.LoadAll => new LoadUsersCommand(userListVm, _userApiClient, _publicUserViewModelFactory),
				_ => new LoadUsersByIdsCommand(userListVm, _userApiClient, _publicUserViewModelFactory),
			};
		}
	}
}
