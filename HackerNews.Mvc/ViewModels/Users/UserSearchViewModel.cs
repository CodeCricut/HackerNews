using HackerNews.Domain.Common.Models.Users;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.Users
{
	public class UserSearchViewModel
	{
		public string SearchTerm { get; set; }
		public FrontendPage<GetPublicUserModel> UserPage { get; set; }
	}
}
