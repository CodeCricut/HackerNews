using Hackernews.WPF.Core;
using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.Domain.Common.Models.Users;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class UserListViewModel : EntityListViewModel<PublicUserViewModel, GetPublicUserModel>
	{
		public UserListViewModel(CreateBaseCommand<EntityListViewModel<PublicUserViewModel, GetPublicUserModel>> createLoadCommand) : base(createLoadCommand)
		{
		}
	}
}
