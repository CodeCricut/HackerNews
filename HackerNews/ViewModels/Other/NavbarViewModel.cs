using HackerNews.Domain.Models.Users;

namespace HackerNews.ViewModels.Other
{
	public class NavbarViewModel
	{
		public bool LoggedIn
		{
			get
			{
				return User != null && User.Id != 0;
			}
		}
		public GetPrivateUserModel User { get; set; }
	}
}
