using Hackernews.WPF.Factories.Commands;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Common;

namespace Hackernews.WPF.Factories.ViewModels
{
	public interface IUserListViewModelFactory
	{
		UserListViewModel Create(LoadEntityListEnum loadEntityType);
	}

	public class UserListViewModelFactory : IUserListViewModelFactory
	{
		private readonly ILoadUsersCommandFactory _loadUserCommandFactory;

		public UserListViewModelFactory(ILoadUsersCommandFactory loadUserCommandFactory)
		{
			_loadUserCommandFactory = loadUserCommandFactory;
		}

		public UserListViewModel Create(LoadEntityListEnum loadEntityType)
			=> new UserListViewModel(createLoadCommand: entityVm => _loadUserCommandFactory.Create(entityVm, loadEntityType));
	}
}
