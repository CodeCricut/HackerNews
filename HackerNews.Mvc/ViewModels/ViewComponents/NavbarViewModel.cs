using HackerNews.Domain.Common.Models.Users;

namespace HackerNews.Mvc.ViewModels.ViewComponents
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
