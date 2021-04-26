using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.WPF.Core.Commands;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class UserListViewModel : EntityListViewModel<PublicUserViewModel, GetPublicUserModel>
	{
		public UserListViewModel(CreateBaseCommand<EntityListViewModel<PublicUserViewModel, GetPublicUserModel>> createLoadCommand) : base(createLoadCommand)
		{
		}
	}
}
